    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaConfiguracaoHistoricoApplication<TContext> : ApplicationBase<TContext, EntregaConfiguracaoHistorico>, IEntregaConfiguracaoHistoricoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaConfiguracaoHistoricoApplication(IUnitOfWork<TContext> context, IEntregaConfiguracaoHistoricoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
