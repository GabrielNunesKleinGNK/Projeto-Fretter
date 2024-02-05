using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class FaturaConciliacaoService<TContext> : ServiceBase<TContext, FaturaConciliacao>, IFaturaConciliacaoService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IFaturaConciliacaoRepository<TContext> _repository;
        public FaturaConciliacaoService
        (
            IFaturaConciliacaoRepository<TContext> repository, 
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user
        ) 
            : base(repository, unitOfWork, user)
        {
            _repository = repository;
        }

        public RetornoFaturaConciliacaoIntegracaoDTO GetAllFaturaConciliacaoIntegracao(int faturaId)
        {
            var lista = _repository.GetAllFaturaConciliacaoIntegracao(faturaId, _user.UsuarioLogado.EmpresaId) ?? new List<FaturaConciliacaoIntegracaoDTO>();

            decimal somatoriaValor = 0;

            foreach (var item in lista)
            {
                somatoriaValor += item.ValorFrete;
            }

            return new RetornoFaturaConciliacaoIntegracaoDTO(lista, somatoriaValor, lista?.Max(x => x.DataEnvio));
        }

        public JsonIntegracaoFaturaConciliacaoDTO GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId)
        {
            var jsonIntegracao = _repository.GetJsonIntegracaoFaturaConciliacao(empresaIntegracaoItemDetalheId) ?? new JsonIntegracaoFaturaConciliacaoDTO();

            if (jsonIntegracao.EmpresaIntegracaoItemDetalheId == 0)
                jsonIntegracao.EmpresaIntegracaoItemDetalheId = empresaIntegracaoItemDetalheId;

            if (!string.IsNullOrEmpty(jsonIntegracao.JsonRetorno))
                jsonIntegracao.JsonRetorno = FormataJsonRetorno(jsonIntegracao.JsonRetorno);            

            return jsonIntegracao;
        }

        public RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoIndividual(FaturaConciliacaoReenvioDTO conciliacaoReenvioDto)
        {
            RetornoReenvioFaturaConciliacaoDTO retornoReenvioConciliacao = new RetornoReenvioFaturaConciliacaoDTO();

            try
            {
                conciliacaoReenvioDto.UsuarioCadastro = _user.UsuarioLogado.Id;

                var lista = new[] { new { Cd_Id = conciliacaoReenvioDto.FaturaConciliacaoId } };

                FaturaConciliacaoIntegracaoDTO resultado = _repository.ExisteAlgumaReincidencia(CollectionHelper.ConvertTo(lista));

                if (resultado.FaturaConciliacaoId > 0)
                {
                    retornoReenvioConciliacao.Mensagem = "Um ou mais itens dessa tentativa já foi encaminhado para reenvio há menos de 30 minutos";
                    return retornoReenvioConciliacao;
                }

                var conciliacoesReenvio = new List<FaturaConciliacaoReenvioDTO>()
                {
                    new FaturaConciliacaoReenvioDTO()
                    {
                        FaturaConciliacaoId = conciliacaoReenvioDto.FaturaConciliacaoId,
                        FaturaId = conciliacaoReenvioDto.FaturaId,
                        ConciliacaoId = conciliacaoReenvioDto.ConciliacaoId,
                        UsuarioCadastro = conciliacaoReenvioDto.UsuarioCadastro
                    }
                };

                _repository.InserirTabelaReenvio(CollectionHelper.ConvertTo(conciliacoesReenvio));

                retornoReenvioConciliacao.Mensagem = "Reenvio realizado com sucesso";
                retornoReenvioConciliacao.Sucesso = true;
                retornoReenvioConciliacao.DataReenvio = DateTime.Now;
            }
            catch (Exception ex)
            {
                retornoReenvioConciliacao.Mensagem = $"Erro Interno no Servidor {ex.Message}";
            }

            return retornoReenvioConciliacao;
        }

        public RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoMassivo(List<FaturaConciliacaoReenvioDTO> lstConciliacaoReenvio)
        {
            RetornoReenvioFaturaConciliacaoDTO retornoReenvioConciliacao = new RetornoReenvioFaturaConciliacaoDTO();

            try
            {
                lstConciliacaoReenvio.ForEach(x => x.UsuarioCadastro = _user.UsuarioLogado.Id);

                var lista = lstConciliacaoReenvio.Select(x => new { Cd_Id = x.FaturaConciliacaoId }).ToList();

                FaturaConciliacaoIntegracaoDTO resultado = _repository.ExisteAlgumaReincidencia(CollectionHelper.ConvertTo(lista));

                if (resultado.FaturaConciliacaoId > 0)
                {
                    retornoReenvioConciliacao.Mensagem = "Um ou mais itens dessa tentativa já foi encaminhado para reenvio há menos de 30 minutos";
                    return retornoReenvioConciliacao;
                }

                _repository.InserirTabelaReenvio(CollectionHelper.ConvertTo(lstConciliacaoReenvio));

                retornoReenvioConciliacao.Mensagem = "Reenvio realizado com sucesso";
                retornoReenvioConciliacao.Sucesso = true;
                retornoReenvioConciliacao.DataReenvio = DateTime.Now;

            }
            catch (Exception ex)
            {
                retornoReenvioConciliacao.Mensagem = $"Erro Interno no Servidor {ex.Message}";
            }

            return retornoReenvioConciliacao;
        }

        private string FormataJsonRetorno(string jsonRetorno)
        {
            var primeiraLetra = jsonRetorno?.First();
            var ultimaLetra = jsonRetorno?.Last();

            if (primeiraLetra == '"' && ultimaLetra == '"')
                jsonRetorno = jsonRetorno.Substring(1, jsonRetorno.Length - 2);

            if (jsonRetorno.Contains(@"\n"))
                jsonRetorno = jsonRetorno.Replace(@"\n", string.Empty);

            return jsonRetorno.Replace(@"\", string.Empty);
        }
    }
}
