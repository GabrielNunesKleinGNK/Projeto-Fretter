using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IPedidoPendenteTransportadorApplication<TContext> : IApplicationBase<TContext, PedidoPendenteTransportador>
        where TContext : IUnitOfWork<TContext>
    {

        Task<int> ProcessarPedidoPendenteTransportador();
    }
}
