using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class PedidoPendenteTransportadorApplication<TContext> : ApplicationBase<TContext, PedidoPendenteTransportador>, IPedidoPendenteTransportadorApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IPedidoPendenteTransportadorService<TContext> _service;

        public PedidoPendenteTransportadorApplication(IUnitOfWork<TContext> context, IPedidoPendenteTransportadorService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public Task<int> ProcessarPedidoPendenteTransportador()
        {
            return _service.ProcessarTransportadoraPedidoStatus();
        }

    }
}
