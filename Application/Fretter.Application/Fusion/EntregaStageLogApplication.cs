    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageLogApplication<TContext> : ApplicationBase<TContext, EntregaStageLog>, IEntregaStageLogApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageLogApplication(IUnitOfWork<TContext> context, IEntregaStageLogService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
