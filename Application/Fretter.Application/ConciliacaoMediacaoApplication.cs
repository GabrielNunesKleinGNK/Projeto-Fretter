    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoMediacaoApplication<TContext> : ApplicationBase<TContext, ConciliacaoMediacao>, IConciliacaoMediacaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConciliacaoMediacaoApplication(IUnitOfWork<TContext> context, IConciliacaoMediacaoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
