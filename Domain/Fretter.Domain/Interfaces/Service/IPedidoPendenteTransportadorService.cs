using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IPedidoPendenteTransportadorService<TContext> : IServiceBase<TContext, PedidoPendenteTransportador>
       where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarTransportadoraPedidoStatus();
    }
}
