    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageDestinatarioApplication<TContext> : ApplicationBase<TContext, EntregaStageDestinatario>, IEntregaStageDestinatarioApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageDestinatarioApplication(IUnitOfWork<TContext> context, IEntregaStageDestinatarioService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
