using Fretter.Domain.Config;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.CTe;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Dto.MenuFrete;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class ConciliacaoService<TContext> : ServiceBase<TContext, Conciliacao>, IConciliacaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IConciliacaoRepository<TContext> _repository;
        private readonly IConciliacaoStatusRepository<TContext> _conciliacaoStatusRepository;
        private readonly IMessageBusService<ConciliacaoService<TContext>> _messageBusService;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly RecotacaoFreteConfig _recotacaoFreteConfig;

        public ConciliacaoService(IConciliacaoRepository<TContext> Repository,
                                  IMessageBusService<ConciliacaoService<TContext>> messageBusService,
                                  IConciliacaoStatusRepository<TContext> conciliacaoStatusRepository,
                                  IOptions<RecotacaoFreteConfig> recotacaoFreteConfig,
                                  IOptions<MessageBusConfig> messageBusConfig,
                                  IUnitOfWork<TContext> unitOfWork,
                                  IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _repository = Repository;
            _conciliacaoStatusRepository = conciliacaoStatusRepository;
            _recotacaoFreteConfig = recotacaoFreteConfig.Value;

            _messageBusConfig = messageBusConfig.Value;
            _messageBusService = messageBusService;
            _messageBusService.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.EntregaConciliacaoTopic);
        }

        public List<RelatorioDetalhadoDTO> ObterRelatorioDetalhado(RelatorioDetalhadoFiltroDTO filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            var result = _repository.ExecuteStoredProcedure<RelatorioDetalhadoDTO, RelatorioDetalhadoFiltroDTO>(filtro, "Fretter.GetRelatorioDetalhado",250);

            List<long> lstConciliacoesId = new List<long>();

            result?.ForEach(res =>
            {
                res.ListComposicaoCte = new List<ItemComposicaoDto>();
                res.ListComposicaoRecotacao = new List<ItemComposicaoDto>();

                if (!string.IsNullOrEmpty(res.JsonValoresCte))
                    res.ListComposicaoCte = JsonConvert.DeserializeObject<List<ItemComposicaoDto>>(res.JsonValoresCte);
                if (!string.IsNullOrEmpty(res.JsonValoresRecotacao))
                    res.ListComposicaoRecotacao = JsonConvert.DeserializeObject<List<ItemComposicaoDto>>(res.JsonValoresRecotacao);

                lstConciliacoesId.Add(res.CodigoConciliacao);
            });

            var conciliacoes = lstConciliacoesId.Select(conciliacaoId => new { Id = conciliacaoId }).ToList();

            List<ConciliacaoRecotacaoDTO> recotacoes = _repository.ListarRecotacoesPorIds(CollectionHelper.ConvertTo(conciliacoes));

            result?.ForEach(res =>
            {
                res.ListRecotacoes = recotacoes.Where(recotacao => recotacao.ConciliacaoId == res.CodigoConciliacao).ToList();
            });

            return result;
        }

        public List<RelatorioDetalhadoArquivoDTO> ObterRelatorioDetalhadoArquivo(RelatorioDetalhadoFiltroDTO filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            var result = _repository.ExecuteStoredProcedure<RelatorioDetalhadoArquivoDTO, RelatorioDetalhadoFiltroDTO>(filtro, "Fretter.GetRelatorioDetalhadoArquivo",150);
            return result;
        }

        public List<ConciliacaoStatus> ObterStatus()
        {
            var result = _conciliacaoStatusRepository.GetAll().ToList();
            return result;
        }

        public void ProcessaConciliacao() => _repository.ProcessaConciliacao();    
        
        public void ProcessaFatura() => _repository.ProcessaFatura();

        public async Task<int> ProcessaConciliacaoControle()
        {
            int qtdEntregas;
            int loteEntregas;
            Int64 idControleProcesso = 0;
            int loteBatchEntregas = 50, loteBatchSkip = 0;
            var entregas = _repository.ProcessaConciliacaoControle();
            qtdEntregas = entregas.Count();
            idControleProcesso = qtdEntregas > 0 ? (entregas.FirstOrDefault().ControleProcessoConciliacaoId ?? 0) : 0;

            if (qtdEntregas > 0)
            {
                loteEntregas = (qtdEntregas / loteBatchEntregas) + 1;
                for (int i = 0; i < loteEntregas; i++)
                {
                    await _messageBusService.SendRange<EntregaConciliacaoDTO>(entregas.Skip(loteBatchSkip).Take(loteBatchEntregas).ToList());
                    loteBatchSkip += loteBatchEntregas;
                }
            }

            _repository.ProcessaConciliacaoControle(idControleProcesso);

            return qtdEntregas;
        }

        public async Task<int> ProcessaConciliacaoRecotacao()
        {
            var entregasRetorno = new List<ConciliacaoRecotacaoInsereDTO>();
            var entregas = _repository.ObterConciliacaoRecotacao();
            entregas.GroupBy(t => t.EntregaId).Select(ent => ent.OrderBy(p => p.EntregaId)).ForEach(entregas =>
            {
                var entregaPrincipal = entregas.FirstOrDefault();
                var carrinho = new CarrinhoDTO
                {
                    canalVenda = entregaPrincipal.CanalVenda,
                    seller_id = entregaPrincipal.CanalId,
                    cepDestino = entregaPrincipal.CepDestino,
                    tipoServico = entregaPrincipal.TipoServico,
                    cdPedido = entregaPrincipal.CodigoPedido,
                    vlrCarrinho = entregas.Count() <= 1 ? entregas.Sum(t => t.Valor * t.Quantidade) : entregas.Sum(t => t.Valor),
                    itens = entregas.Select(t => new CarrinhoItemDTO
                    {
                        cnpjCanal = t.CanalCnpj,
                        sku = t.CodigoSku,
                        cdItem = t.CodigoItem,
                        altura = _recotacaoFreteConfig.RecotaConciliacaoPorPeso ? null : t.Altura,
                        largura = _recotacaoFreteConfig.RecotaConciliacaoPorPeso ? null : t.Largura,
                        comprimento = _recotacaoFreteConfig.RecotaConciliacaoPorPeso ? null : t.Comprimento,
                        peso = t.Peso,
                        vlrItem = t.Valor,
                        qtdItem = t.Quantidade,
                    }).ToArray(),
                    microServicoId = entregaPrincipal.MicroServicoId,
                };

                //Chamada novo endpoint
                var webApiClient = new WebApiClient(_recotacaoFreteConfig.EndpointCompleto);
                webApiClient.AddHeader("Token", entregaPrincipal.Token);
                var response = webApiClient.PostWithHeader<CarrinhoDTO>(_recotacaoFreteConfig.EndpointCompleto, carrinho).Result;

                entregasRetorno.Add(FormataRetornoConciliacao(response, entregaPrincipal.ConciliacaoRecotacaoId, entregaPrincipal.ConciliacaoId).GetAwaiter().GetResult());
            });

            if (entregasRetorno.Count > 0)
                _repository.ProcessaConciliacaoRecotacao(CollectionHelper.ConvertTo<ConciliacaoRecotacaoInsereDTO>(entregasRetorno));

            return entregas.Count;
        }

        private async Task<ConciliacaoRecotacaoInsereDTO> FormataRetornoConciliacao(System.Net.Http.HttpResponseMessage response, long conciliacaoRecotacaoId, long conciliacaoId)
        {
            string responseApi = response.Content != null ? await response.Content.ReadAsStringAsync() : "";
            string retornoJson = responseApi;
            var carrinhoResult = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<CarrinhoResultadoDTO>(responseApi) : new CarrinhoResultadoDTO();
            string jsonRecotacao = string.Empty;
            var modalidades = carrinhoResult?.modalidades;
            var composicoes = new List<ItemComposicao>();

            if (modalidades?.Count > 0)
                foreach (var mod in modalidades)
                {
                    foreach (var it in mod?.itens)
                    {
                        foreach (var comp in it?.composicao)
                        {
                            if (composicoes.Any(t => t.chave == comp.chave))
                                composicoes.Where(t => t.chave == comp.chave).FirstOrDefault().valor += comp.valor;
                            else
                                composicoes.Add(new ItemComposicao() { chave = comp.chave, valor = comp.valor, tipo = ValidaTipoComposicaoCte(comp.chave) });
                        }
                    }
                }

            var idTabela = carrinhoResult.modalidades?.FirstOrDefault()?.itens?.FirstOrDefault()?.idTabela;

            return new ConciliacaoRecotacaoInsereDTO()
            {
                ConciliacaoRecotacaoId = conciliacaoRecotacaoId,
                ConciliacaoId = conciliacaoId,
                Sucesso = response.IsSuccessStatusCode,
                JsonRetornoRecotacao = retornoJson,
                Protocolo = carrinhoResult?.protocolo,
                ValorCustoFrete = carrinhoResult?.modalidades?.Sum(t => t.custo),
                JsonValoresRecotacao = JsonConvert.SerializeObject(composicoes),
                TabelaId = (idTabela != null && idTabela < 0 ? (idTabela * -1) : idTabela)
            };
        }

        private EnumCteComposicaoTipo ValidaTipoComposicaoCte(string chave)
        {
            EnumCteComposicaoTipo tipo;
            switch (chave.ToLower())
            {
                case "pesoconsiderado":
                    tipo = EnumCteComposicaoTipo.Peso;
                    break;
                default:
                    tipo = EnumCteComposicaoTipo.Valor;
                    break;
            }
            return tipo;
        }

        public string ObterArquivoPorConciliacao(int conciliacaoId)
        {
            var importacaoArquivo = _repository.GetArquivoPorConciliacao(conciliacaoId);
            return importacaoArquivo?.Diretorio;
        }

        public void EnviarParaRecalculoFrete(List<int> listaConciliacoes)
        {
            var conciliacoes = listaConciliacoes.Select(conciliacaoId => new { Id = conciliacaoId }).ToList();

            _repository.EnviarParaRecalculoFrete
            (
                CollectionHelper.ConvertTo(conciliacoes), 
                JsonConvert.SerializeObject(conciliacoes), 
                _user.UsuarioLogado.Id
            );
        }

        public void EnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;

            _repository.EnviarParaRecalculoFreteMassivo(filtro, JsonConvert.SerializeObject(filtro), _user.UsuarioLogado.Id);
        }
    }
}
