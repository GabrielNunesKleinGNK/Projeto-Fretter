    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoStatusApplication<TContext> : ApplicationBase<TContext, ConciliacaoStatus>, IConciliacaoStatusApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConciliacaoStatusApplication(IUnitOfWork<TContext> context, IConciliacaoStatusService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
