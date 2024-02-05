    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class CanalApplication<TContext> : ApplicationBase<TContext, Canal>, ICanalApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public CanalApplication(IUnitOfWork<TContext> context, ICanalService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
