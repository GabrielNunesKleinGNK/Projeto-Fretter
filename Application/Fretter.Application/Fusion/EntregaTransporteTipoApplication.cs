    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaTransporteTipoApplication<TContext> : ApplicationBase<TContext, EntregaTransporteTipo>, IEntregaTransporteTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaTransporteTipoApplication(IUnitOfWork<TContext> context, IEntregaTransporteTipoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
