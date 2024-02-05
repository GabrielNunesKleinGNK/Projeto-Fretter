
using Fretter.Domain.Dto.RegraEstoque;
using Fretter.Domain.Entities.Fusion.SKU;

namespace Fretter.Domain.Interfaces.Repository.Fusion
{
    public interface IRegraEstoqueRepository<TContext> : IRepositoryBase<TContext, RegraEstoque>
        where TContext : IUnitOfWork<TContext>
    {
        RegraEstoqueDTO SaveWithProcedure(RegraEstoqueDTO regraEstoque);
        RegraEstoque GetByCanalDestino(int idCanalDestino);
    }
}
