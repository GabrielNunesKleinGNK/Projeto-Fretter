using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class FaturaConciliacaoApplication<TContext> : ApplicationBase<TContext, FaturaConciliacao>, IFaturaConciliacaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IFaturaConciliacaoService<TContext> _service;

        public FaturaConciliacaoApplication(IUnitOfWork<TContext> context, IFaturaConciliacaoService<TContext> service) : base(context, service)
        {
            _service = service;
        }

        public RetornoFaturaConciliacaoIntegracaoDTO GetAllFaturaConciliacaoIntegracao(int faturaId)
        {
            return _service.GetAllFaturaConciliacaoIntegracao(faturaId);
        }

        public JsonIntegracaoFaturaConciliacaoDTO GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId)
        {
            return _service.GetJsonIntegracaoFaturaConciliacao(empresaIntegracaoItemDetalheId);
        }

        public RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoIndividual(FaturaConciliacaoReenvioDTO conciliacaoReenvio)
        {
            return _service.ReenviarFaturaConciliacaoIndividual(conciliacaoReenvio);
        }

        public RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoMassivo(List<FaturaConciliacaoReenvioDTO> lstConciliacaoReenvio)
        {
            return _service.ReenviarFaturaConciliacaoMassivo(lstConciliacaoReenvio);
        }
    }
}
