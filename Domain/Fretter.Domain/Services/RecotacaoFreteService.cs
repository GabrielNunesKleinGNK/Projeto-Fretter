using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using System.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using Fretter.Domain.Config;
using Microsoft.Extensions.Options;
using static Fretter.Domain.Helpers.XmlHelper;
using Fretter.Domain.Dto.RecotacaoFrete;
using Newtonsoft.Json;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using System.Diagnostics;

namespace Fretter.Domain.Services
{
    public class RecotacaoFreteService<TContext> : ServiceBase<TContext, RecotacaoFrete>, IRecotacaoFreteService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IRecotacaoFreteRepository<TContext> _repository;
        private readonly IEmpresaTokenRepository<TContext> _repositoryEmpresaToken;
        private readonly IMicroServicoRepository<TContext> _repositoryMicroServico;
        private readonly ICanalVendaRepository<TContext> _repositoryCanalVenda;

        public readonly IBlobStorageService _blobStorageService;
        private readonly BlobStorageConfig _blobConfig;
        private readonly RecotacaoFreteConfig _recotacaoFreteConfig;
        public RecotacaoFreteService(IRecotacaoFreteRepository<TContext> repository,
            IEmpresaTokenRepository<TContext> repositoryEmpresaToken,
            IMicroServicoRepository<TContext> repositoryMicroServico,
            ICanalVendaRepository<TContext> repositoryCanalVenda,
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            IOptions<RecotacaoFreteConfig> recotacaoFreteConfig,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _repositoryEmpresaToken = repositoryEmpresaToken;
            _repositoryMicroServico = repositoryMicroServico;
            _repositoryCanalVenda = repositoryCanalVenda;
            _blobConfig = blobConfig.Value;
            _recotacaoFreteConfig = recotacaoFreteConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.RecotacaoFreteContainerName);
        }

        private static string Endpoint;
        private Stream FileStream { get; set; }

        public async Task<int> ProcessarRecotacaoFrete()
        {
            var recotacaoFrete = _repository.GetAll(a => a.IdRecotacaoFreteStatus == (int)EnumRecotacaoFreteStatus.AguardandoProcessamento).FirstOrDefault();

            if (recotacaoFrete == null)
                return 0;

            Endpoint = _recotacaoFreteConfig.Endpoint;
            var retMsg = new ListImportacaoExcelMsg();

            try
            {
                _repository.AtualizarRecotacaoFrete(recotacaoFrete.Id, EnumRecotacaoFreteStatus.Processando);

                FileStream = await _blobStorageService.GetFile(recotacaoFrete.DsUrl);

                var cotacoes = ObterMenuFreteCotacao(recotacaoFrete, retMsg);

                var resultadoCotacoes = await DispararCotacao(cotacoes, recotacaoFrete);

                _repository.InserirRecotacaoFreteItem(CollectionHelper.ConvertTo(resultadoCotacoes));

                var status = ValidarStatus(retMsg, resultadoCotacoes);

                _repository.AtualizarRecotacaoFrete(recotacaoFrete.Id, status,
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error), JsonConvert.SerializeObject(retMsg));
            }
            catch (Exception e)
            {
                retMsg.Add(e.Message, true, null);
                _repository.AtualizarRecotacaoFrete(recotacaoFrete.Id, EnumRecotacaoFreteStatus.Erro,
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error), JsonConvert.SerializeObject(retMsg));
            }
            finally
            {
                if(FileStream!=null)
                    FileStream.Dispose();
            }
            return 1;
        }

        private static EnumRecotacaoFreteStatus ValidarStatus(ListImportacaoExcelMsg retMsg, List<MenuFreteCotacao> resultadoCotacoes)
        {
            if (!retMsg.Any(c => c.Error) && resultadoCotacoes.Any(a => string.IsNullOrEmpty(a.Ds_Mensagem)))
                return EnumRecotacaoFreteStatus.Processado;

            if (retMsg.All(c => c.Error) && resultadoCotacoes.All(a=> !string.IsNullOrEmpty(a.Ds_Mensagem)))
                return EnumRecotacaoFreteStatus.Erro; 
        
            return  EnumRecotacaoFreteStatus.ProcessadoParcialmente;
        }

        private List<MenuFreteCotacao> ObterMenuFreteCotacao(RecotacaoFrete recotacaoFrete, ListImportacaoExcelMsg retMsg)
        {
            var empresaToken = _repositoryEmpresaToken.GetAll(e => e.EmpresaId == recotacaoFrete.IdEmpresa && e.Ativo)
                .LastOrDefault();
            var listaCotacao = MapearCotacao(retMsg, recotacaoFrete.Id, recotacaoFrete.IdRecotacaoFreteTipo, empresaToken.Token.ToString());

            if (recotacaoFrete.IdRecotacaoFreteTipo == (int)EnumRecotacaoFreteTipo.Cotacao)
            {
                var listMicroServico = listaCotacao.Select(s => s.Ds_MicroServico).Distinct();

                var resultMicroServico = _repositoryMicroServico.GetAll(a => 
                    a.EmpresaTransportadorConfig.EmpresaId == recotacaoFrete.IdEmpresa &&
                    listMicroServico.Contains(a.ServicoCodigo)).ToList();

                CanalVenda canalVenda = recotacaoFrete.IdCanalVenda == null ? null : _repositoryCanalVenda.Get(recotacaoFrete.IdCanalVenda.Value);

                resultMicroServico.ForEach(microServico =>
                {
                    listaCotacao.Where(w => w.Ds_MicroServico == microServico.ServicoCodigo)
                    .ForEach(cotacao => {
                        cotacao.Id_MicroServico = microServico.Id;
                        
                    });
                });
                
                listaCotacao
                    .ForEach(cotacao => {
                        cotacao.Ds_Canal = canalVenda?.CanalVendaNome;
                    });

                return listaCotacao;
            }
                

            var cotacoesPedido = _repository.BuscarDadosPedido(CollectionHelper.ConvertTo(listaCotacao));

            listaCotacao.Where(w => !cotacoesPedido.Any(s => s.Cd_CodigoPedido == w.Cd_CodigoPedido)).
                ForEach(pedido =>
                {
                    var msg = "Pedido não encontrado";
                    retMsg.Add(msg, true, pedido.Linha.ToString(), pedido.Cd_CodigoPedido);
                    pedido.Ds_Mensagem = msg;
                    cotacoesPedido.Add(pedido);
                });

            cotacoesPedido.ForEach(item => item.Ds_Token = empresaToken.Token.ToString());

            return cotacoesPedido;
        }

        private static async Task<List<MenuFreteCotacao>> DispararCotacao(List<MenuFreteCotacao> cotacoes, RecotacaoFrete recotacaoFrete)
        {
            var resultadoCotacoes = new List<MenuFreteCotacao>();
            resultadoCotacoes.AddRange(cotacoes.Where(w => !string.IsNullOrEmpty(w.Ds_Mensagem)));

            await cotacoes.Where(e => e.Cd_EntregaUnificadaId == null && string.IsNullOrEmpty(e.Ds_Mensagem))
                .ForEachAsync(async cotacao => {
                    resultadoCotacoes.Add(await Cotar(cotacao, recotacaoFrete));
            });

            await cotacoes.Where(e => e.Cd_EntregaUnificadaId!=null && string.IsNullOrEmpty(e.Ds_Mensagem))
                .GroupBy(e => e.Cd_EntregaUnificadaId)
                .ForEachAsync(async embalagem =>
            {
                resultadoCotacoes.AddRange(await CotarEmbalagem(embalagem.ToList(), recotacaoFrete));
            });

            return resultadoCotacoes;
        }

        private List<MenuFreteCotacao> MapearCotacao(ListImportacaoExcelMsg retMsg, int idRecotacaoFrete, 
            int tipoCotacao, string token)
        {
            var retorno = new List<MenuFreteCotacao>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pkg = new ExcelPackage(FileStream))
            {
                var ws = pkg.Workbook.Worksheets[0];

                var r = 4;
                while (!string.IsNullOrEmpty(ws.Cells[r, 1]?.Text))
                {
                    var retMsgitem = new ListImportacaoExcelMsg();

                    var cotacao = new MenuFreteCotacao(idRecotacaoFrete, r, token);
                    cotacao.Linha = r;
                    cotacao.Id_RecotacaoFrete = idRecotacaoFrete;
                    var column = 1;

                    switch (tipoCotacao)
                    {
                        case (int)EnumRecotacaoFreteTipo.Pedido:
                            ws.Cells[r, 1].TryFill2(a => cotacao.Cd_CodigoPedido = a, retMsg);
                            break;
                        case (int)EnumRecotacaoFreteTipo.Cotacao:
                            ObterPorTipoCotacao(ws, r, retMsgitem, cotacao, column);
                            break;
                    }
                    r++;

                    if (retMsgitem.Any())
                        retMsg.AddRange(retMsgitem);

                    if (!retMsgitem.Any(a => a.Error))
                        retorno.Add(cotacao);
                }
            }

            return retorno;
        }

        private static void ObterPorTipoCotacao(ExcelWorksheet ws, int r, ListImportacaoExcelMsg retMsgitem, MenuFreteCotacao cotacao, int column)
        {
            ws.Cells[r, column++].TryFill2(a => cotacao.Ds_Modalidade = a, retMsgitem);
            ws.Cells[r, column++].TryFill2(a => cotacao.Ds_CepOrigem = a, retMsgitem);
            ws.Cells[r, column++].TryFill2(a => cotacao.Ds_CepDestino = a, retMsgitem);
            ws.Cells[r, column++].TryFill2(a => cotacao.Ds_SKU = a, retMsgitem);
            cotacao.Nr_Quantidade = Convert.ToInt32(ws.Cells[r, column++].Value);
            ws.Cells[r, column++].TryFillDecimal((decimal a) => cotacao.Nr_Altura = a, retMsgitem);
            ws.Cells[r, column++].TryFillDecimal((decimal a) => cotacao.Nr_Largura = a, retMsgitem);
            ws.Cells[r, column++].TryFillDecimal((decimal a) => cotacao.Nr_Comprimento = a, retMsgitem);
            ws.Cells[r, column++].TryFillDecimal((decimal a) => cotacao.Nr_Peso = a, retMsgitem);
            ws.Cells[r, column++].TryFillDecimal((decimal a) => cotacao.Nr_Valor = a, retMsgitem);
            int.TryParse(ws.Cells[r, column++]?.Value?.ToString()??"", out int sellerId);
            cotacao.Cd_SellerId = sellerId;
            ws.Cells[r, column++].TryFill2(a => cotacao.Ds_MicroServico = a, retMsgitem,obrigatoria: false);
        }

        private static async Task<MenuFreteCotacao> Cotar(MenuFreteCotacao cotacao, RecotacaoFrete recotacaoFrete)
        {
            try
            {
                var cotacaoRequest = new FusionRequest(cotacao, "Entrega");

                cotacaoRequest.itens.Add(new RequestItem(cotacao));

                cotacaoRequest.vlrCarrinho = cotacao.Nr_Valor;

                var watch = new Stopwatch();
                watch.Start();

                var response = await EnviarCotacao(cotacaoRequest, cotacao.Ds_Token);

                if (response != null)
                    PopularRetornoCotacao(watch, response, cotacao, recotacaoFrete);
            }
            catch (Exception e)
            {
                cotacao.Ds_Mensagem = e.Message;
                return cotacao;
            }
            return cotacao;
        }

        private static async Task<List<MenuFreteCotacao>> CotarEmbalagem(List<MenuFreteCotacao> cotacoes, RecotacaoFrete recotacaoFrete)
        {
            int item = 1;
            try
            {
                var token = cotacoes.FirstOrDefault().Ds_Token;
                var cotacaoRequest = new FusionRequest(cotacoes.FirstOrDefault(), "Entrega");
                
                cotacoes.ForEach(cotacao =>
                {
                    cotacaoRequest.itens.Add(new RequestItem(cotacao, item));
                    item++;
                });

                cotacaoRequest.vlrCarrinho = cotacoes.Sum(e => e.Nr_Valor);

                var watch = new Stopwatch();
                watch.Start();
                var response = await EnviarCotacao(cotacaoRequest, token);//"ce94c3db-c657-4ad9-bc1a-81957fca4837");

                if (response != null)
                {
                    item = 1;
                    foreach (var cotacao in cotacoes)
                    {
                        PopularRetornoCotacao(watch, response, cotacao, recotacaoFrete, item);
                        item++;
                    }
                }
            }
            catch (Exception e)
            {
                foreach (var cotacao in cotacoes)
                {
                    if (cotacao.Nr_Peso <= 0)
                        cotacao.Ds_Mensagem = "peso zerado. Obrigatorio informar um valor > zero";
                    else
                        cotacao.Ds_Mensagem = e.Message;
                }
            }
            return cotacoes;
        }

        private static void PopularRetornoCotacao(Stopwatch watch, FusionResponse response, MenuFreteCotacao cotacao, RecotacaoFrete recotacaoFrete, int? item = null)
        {
            cotacao.Nr_Tempo = watch.ElapsedMilliseconds;
            cotacao.Ds_Protocolo = response.protocolo;
            cotacao.Ds_Mensagem = response.msg;

            if (response.modalidades != null && response.modalidades.Any())
            {
                Modalidade modalidade = null;

                if (recotacaoFrete.PriorizarPrazo)
                    modalidade = response?.modalidades.OrderBy(o => o.prazo).FirstOrDefault();
                else
                    modalidade = response?.modalidades.OrderBy(o => o.custo).FirstOrDefault();

                cotacao.Ds_Transportador = modalidade?.transportador;
                cotacao.Ds_Grupo = modalidade?.idTransportador.ToString();
                cotacao.Ds_ModalidadeRetorno = modalidade?.modalidade;
                cotacao.Nr_Prazo = (decimal)modalidade?.prazo;
                cotacao.Nr_PrazoExpedicao = (decimal)modalidade?.prazoExpedicao;
                cotacao.Nr_PrazoTransportador = (decimal)modalidade?.prazoTransit;

                if (item == null)
                {
                    cotacao.Nr_ValorCusto = (decimal)modalidade?.custo;
                    cotacao.Nr_ValorReceita = (decimal)modalidade?.valor;
                }
                else
                {
                    var resultItem = modalidade?.itens.FirstOrDefault(e => e.cdItem == $"{cotacao.Ds_SKU}_{item}");
                    cotacao.Nr_ValorCusto = (decimal)resultItem.custo;
                    cotacao.Nr_ValorReceita = (decimal)resultItem.valor;
                }
            }
            else
                cotacao.Ds_Mensagem = "Nenhuma modalide retornada";

            cotacao.Nr_Margem = cotacao.Nr_ValorReceita - cotacao.Nr_ValorCusto;
        }

        private static async Task<FusionResponse> EnviarCotacao(FusionRequest cotacaoRequest, string token)
        {
            var webApiClient = new WebApiClient(Endpoint);
            webApiClient.IsAnonymous = true;

            var header = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Token", token),
                };

            return await webApiClient.PostWithHeader<FusionResponse>(Endpoint,
                cotacaoRequest, null, header);
        }

    }
}