using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repositories;

namespace Fretter.Repository.Repositories
{
    public class EntregaStageCallBackRepository<TContext> : RepositoryBase<TContext, EntregaStageCallBack>, IEntregaStageCallBackRepository<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        public EntregaStageCallBackRepository(IUnitOfWork<TContext> context) : base(context)
        {
        }
    }
}
