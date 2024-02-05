    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoHistoricoApplication<TContext> : ApplicationBase<TContext, ConciliacaoHistorico>, IConciliacaoHistoricoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConciliacaoHistoricoApplication(IUnitOfWork<TContext> context, IConciliacaoHistoricoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
