using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageSkuService<TContext> : IServiceBase<TContext, EntregaStageSku>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
