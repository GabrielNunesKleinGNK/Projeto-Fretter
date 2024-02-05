using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Fusion;

namespace Fretter.Application
{
    public class EntregaIncidenteApplication<TContext> : ApplicationBase<TContext, EntregaStage>, IEntregaIncidenteApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaIncidenteService<TContext> _service;
        public EntregaIncidenteApplication(IUnitOfWork<TContext> context,
            IEntregaIncidenteService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
 
        public async Task ProcessarFilaIncidentes()
        {
            await _service.ProcessarFilaIncidentes();
        }
    }
}
