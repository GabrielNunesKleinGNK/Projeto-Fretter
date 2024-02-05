    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaConfiguracaoTipoApplication<TContext> : ApplicationBase<TContext, EntregaConfiguracaoTipo>, IEntregaConfiguracaoTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaConfiguracaoTipoApplication(IUnitOfWork<TContext> context, IEntregaConfiguracaoTipoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
