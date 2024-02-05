using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoStatusService<TContext> : IServiceBase<TContext, EntregaDevolucaoStatus>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
