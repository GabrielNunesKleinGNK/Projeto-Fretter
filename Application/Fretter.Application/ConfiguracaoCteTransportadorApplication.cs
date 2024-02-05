    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConfiguracaoCteTransportadorApplication<TContext> : ApplicationBase<TContext, ConfiguracaoCteTransportador>, IConfiguracaoCteTransportadorApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConfiguracaoCteTransportadorApplication(IUnitOfWork<TContext> context, IConfiguracaoCteTransportadorService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
