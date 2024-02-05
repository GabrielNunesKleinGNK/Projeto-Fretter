
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConfiguracaoApplication<TContext> : ApplicationBase<TContext, Configuracao>, IConfiguracaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConfiguracaoApplication(IUnitOfWork<TContext> context, IConfiguracaoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
