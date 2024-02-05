using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class PedidoPendenteBSellerApplication<TContext> : ApplicationBase<TContext, PedidoPendenteBSeller>, IPedidoPendenteBSellerApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IPedidoPendenteBSellerService<TContext> _service;

        public PedidoPendenteBSellerApplication(IUnitOfWork<TContext> context, IPedidoPendenteBSellerService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public Task<int> ProcessarBatimentoPedidoBSeller()
        {
            return _service.ProcessarPedidoPendenteBSeller();
        }
    }
}
