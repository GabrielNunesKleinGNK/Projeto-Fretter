    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageErroApplication<TContext> : ApplicationBase<TContext, EntregaStageErro>, IEntregaStageErroApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageErroApplication(IUnitOfWork<TContext> context, IEntregaStageErroService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
