    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaDevolucaoStatusApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoStatus>, IEntregaDevolucaoStatusApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaDevolucaoStatusApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoStatusService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
