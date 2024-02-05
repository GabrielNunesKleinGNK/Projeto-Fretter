using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IMicroServicoService<TContext> : IServiceBase<TContext, MicroServico>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
