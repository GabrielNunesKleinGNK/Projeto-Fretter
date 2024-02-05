using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageLogService<TContext> : IServiceBase<TContext, EntregaStageLog>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
