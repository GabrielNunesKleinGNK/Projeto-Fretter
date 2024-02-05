using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoTransportadorApplication<TContext> : ApplicationBase<TContext, EntregaConciliacao>, IConciliacaoTransportadorApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        readonly IConciliacaoTransportadorService<TContext> _service;
        public ConciliacaoTransportadorApplication(IUnitOfWork<TContext> context, IConciliacaoTransportadorService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }       

        public Task<int> ProcessaConciliacaoTransportador() => this._service.ProcessaConciliacaoTransportador();
    }
}
