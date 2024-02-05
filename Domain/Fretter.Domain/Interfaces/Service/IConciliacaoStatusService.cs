using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConciliacaoStatusService<TContext> : IServiceBase<TContext, ConciliacaoStatus>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
