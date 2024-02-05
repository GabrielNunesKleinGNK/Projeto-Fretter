    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageRemetenteApplication<TContext> : ApplicationBase<TContext, EntregaStageRemetente>, IEntregaStageRemetenteApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageRemetenteApplication(IUnitOfWork<TContext> context, IEntregaStageRemetenteService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
