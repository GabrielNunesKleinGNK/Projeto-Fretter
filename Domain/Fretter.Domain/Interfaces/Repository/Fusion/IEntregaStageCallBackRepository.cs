using System.Data;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;

namespace Fretter.Domain.Interfaces.Repositories
{
    public interface IEntregaStageCallBackRepository<TContext> : IRepositoryBase<TContext, EntregaStageCallBack>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
