using Fretter.Domain.Entities.Fretter;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IPedidoPendenteTransportadorRepository<TContext> : IRepositoryBase<TContext, PedidoPendenteTransportador>
        where TContext : IUnitOfWork<TContext>
    {
        void SalvarPedidoPendenteTransportador(DataTable listaPedidoPendenteTransportador);
    }
}
