using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class FaturaHistoricoApplication<TContext> : ApplicationBase<TContext, FaturaHistorico>, IFaturaHistoricoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IFaturaHistoricoService<TContext> _service;
        public FaturaHistoricoApplication(IUnitOfWork<TContext> context, IFaturaHistoricoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public List<FaturaHistorico> GetHistoricoDeFaturasPorEmpresa(int faturaId)
        {
            return _service.GetHistoricoDeFaturasPorEmpresa(faturaId).Result;
        }
    }
}
