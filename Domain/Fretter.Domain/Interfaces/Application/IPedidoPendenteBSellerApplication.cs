using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IPedidoPendenteBSellerApplication<TContext> : IApplicationBase<TContext, PedidoPendenteBSeller>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarBatimentoPedidoBSeller();
    }
}
