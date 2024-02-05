
using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class AnymarketApplication<TContext> : ApplicationBase<TContext, EntregaConfiguracao>, IAnymarketApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public readonly IAnymarketService<TContext> _anymarketService;
        public AnymarketApplication(IUnitOfWork<TContext> context, IAnymarketService<TContext> service) 
            : base(context, service)
        {
            _anymarketService = service;
        }

        public async Task ImportarSku()
        {
            await _anymarketService.ImportarSku();
        }
    }
}	
