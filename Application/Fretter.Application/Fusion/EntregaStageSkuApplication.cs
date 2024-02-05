    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageSkuApplication<TContext> : ApplicationBase<TContext, EntregaStageSku>, IEntregaStageSkuApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageSkuApplication(IUnitOfWork<TContext> context, IEntregaStageSkuService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
