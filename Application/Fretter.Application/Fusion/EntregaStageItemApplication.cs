    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageItemApplication<TContext> : ApplicationBase<TContext, EntregaStageItem>, IEntregaStageItemApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageItemApplication(IUnitOfWork<TContext> context, IEntregaStageItemService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
