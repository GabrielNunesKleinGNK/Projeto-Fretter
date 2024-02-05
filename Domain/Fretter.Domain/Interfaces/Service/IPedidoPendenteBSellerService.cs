using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IPedidoPendenteBSellerService<TContext> : IServiceBase<TContext, PedidoPendenteBSeller>
       where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarPedidoPendenteBSeller();
    }
}
