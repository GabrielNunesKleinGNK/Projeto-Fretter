
using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaConfiguracaoApplication<TContext> : ApplicationBase<TContext, EntregaConfiguracao>, IEntregaConfiguracaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public readonly IEntregaConfiguracaoService<TContext> _entregaConfiguracaoService;
        public EntregaConfiguracaoApplication(IUnitOfWork<TContext> context, IEntregaConfiguracaoService<TContext> service) 
            : base(context, service)
        {
            _entregaConfiguracaoService = service;
        }

        public async Task ProcessaEntregaConfiguracaoAtivo()
        {
            await _entregaConfiguracaoService.ProcessaEntregaConfiguracaoAtivo();
        }

        public async Task ReprocessaEntregaMirakl()
        {
            await _entregaConfiguracaoService.ReprocessaEntregaMirakl();
        }
    }
}	
