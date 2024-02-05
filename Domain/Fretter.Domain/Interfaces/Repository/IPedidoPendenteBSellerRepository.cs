using Fretter.Domain.Dto.PedidoPendenteBSeller;
using Fretter.Domain.Entities.Fretter;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IPedidoPendenteBSellerRepository<TContext> : IRepositoryBase<TContext, PedidoPendenteBSeller>
        where TContext : IUnitOfWork<TContext>
    {
        void SalvarPedidoPendenteBSeller(DataTable listaPedidoPendenteBeSeller);
        List<EntregaPedido> BuscarEntregaPedido(DataTable itemEntrega);
        List<PedidoStatus> BuscarPedidoStatus(int tipoStatus);
        List<EmpresaProcessamento> BuscarEmpresaProcessamento();
    }
}
