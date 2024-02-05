using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities.Fusion.SKU;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IRegraEstoqueService<TContext> : IServiceBase<TContext, RegraEstoque>
        where TContext : IUnitOfWork<TContext>
    {

    }
}
