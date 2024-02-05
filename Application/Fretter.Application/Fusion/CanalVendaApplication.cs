    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class CanalVendaApplication<TContext> : ApplicationBase<TContext, CanalVenda>, ICanalVendaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public CanalVendaApplication(IUnitOfWork<TContext> context, ICanalVendaService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
