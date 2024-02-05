using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IStageConfigRepository<TContext> : IRepositoryBase<TContext, StageConfig>
        where TContext : IUnitOfWork<TContext>
    {
        List<StageConfig> GetStageConfig();
    }
}
