
using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class VtexApplication<TContext> : ApplicationBase<TContext, EntregaConfiguracao>, IVtexApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public readonly IVtexService<TContext> _vtexService;
        public VtexApplication(IUnitOfWork<TContext> context, IVtexService<TContext> service)
            : base(context, service)
        {
            _vtexService = service;
        }

        public async Task ImportarSku()
        {
            await _vtexService.ImportarSku();
        }
    }
}
