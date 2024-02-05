    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageEntradaApplication<TContext> : ApplicationBase<TContext, EntregaStageEntrada>, IEntregaStageEntradaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageEntradaApplication(IUnitOfWork<TContext> context, IEntregaStageEntradaService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
