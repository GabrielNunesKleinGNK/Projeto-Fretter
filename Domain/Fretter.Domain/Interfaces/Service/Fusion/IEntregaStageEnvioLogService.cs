using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageEnvioLogService<TContext> : IServiceBase<TContext, EntregaStageEnvioLog>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
