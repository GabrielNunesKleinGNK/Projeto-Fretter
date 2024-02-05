using AtendeClienteWs;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EmpresaTransporteConfiguracaoService<TContext> : ServiceBase<TContext, EmpresaTransporteConfiguracao>, IEmpresaTransporteConfiguracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {        
        private readonly IEmpresaTransporteConfiguracaoItemService<TContext> _ServiceConfiguracaoItem;

        public EmpresaTransporteConfiguracaoService(IEmpresaTransporteConfiguracaoRepository<TContext> Repository,
            IEmpresaTransporteConfiguracaoItemService<TContext> ServiceConfiguracaoItem,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user)
            : base(Repository, unitOfWork, user)
        {            
            _ServiceConfiguracaoItem = ServiceConfiguracaoItem;
        }
        public async Task<EmpresaTransporteConfiguracao> TesteIntegracao(EmpresaTransporteConfiguracao dadosParaConsulta)
        {
            var configs = _repository.Get(dadosParaConsulta.Id);

            try
            {
                var client = new AtendeClienteClient();
                var response = client.buscaClienteAsync(configs.CodigoContrato, configs.CodigoCartao, configs.Usuario, configs.Senha).Result.@return;

                var cartao = response.contratos?.FirstOrDefault()?.cartoesPostagem;

                IncluirServicosDoContrato(cartao?.FirstOrDefault()?.servicos, configs.Id);
                configs.AtualizarRetornoValidacao($"Autenticacao realizada com sucesso. Cartao {configs.CodigoCartao} com {cartao?.FirstOrDefault()?.servicos?.Count()} servicos importados.");
                configs.AtualizarValido(true);
                configs.AtualizarVigenciaInicial(cartao.FirstOrDefault()?.dataVigenciaInicio);
                configs.AtualizarVigenciaFinal(cartao.FirstOrDefault()?.dataVigenciaFim);
                configs.AtualizarValidacao(System.DateTime.Now);
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains($"A autenticacao de {configs.Usuario} falhou!"))
                {
                    configs.AtualizarRetornoValidacao($"Falha na consulta. Message: {ex.Message} {ex.StackTrace}");
                    configs.AtualizarValido(false);
                    configs.AtualizarValidacao(System.DateTime.Now);
                }
                else
                {

                    configs.AtualizarRetornoValidacao($"Falha de autenticacao, o retorno foi: {ex.Message} {ex.StackTrace}");
                    configs.AtualizarValido(true);
                    configs.AtualizarValidacao(System.DateTime.Now);
                }
            }

            _repository.Update(configs);            
            return configs;
        }

        private void IncluirServicosDoContrato(servicoERP[] servicos, int empresaTransporteConfiguracaoId)
        {
            var listaDeServicosImportados = _ServiceConfiguracaoItem.GetAll(x => x.EmpresaTransporteConfiguracaoId == empresaTransporteConfiguracaoId);
            List<EmpresaTransporteConfiguracaoItem> items = new List<EmpresaTransporteConfiguracaoItem>();
            foreach (var servico in servicos)
            {
                if (listaDeServicosImportados.Where(x => x.CodigoServico.Trim() == servico.codigo.Trim()).Count() == 0)
                {
                    EmpresaTransporteConfiguracaoItem item = new
                        EmpresaTransporteConfiguracaoItem(0, empresaTransporteConfiguracaoId, servico.codigo, servico.servicoSigep.categoriaServico.GetDisplayValue(),
                                                          servico.id.ToString(), servico.descricao, servico.dataAtualizacao, servico.vigencia.dataInicial, servico.vigencia.dataFinal,
                                                          DateTime.Now);

                    items.Add(item);
                }
            }

            _ServiceConfiguracaoItem.SaveRange(items);
        }
    }
}
