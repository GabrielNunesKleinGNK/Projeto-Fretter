using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConfiguracaoCteTransportadorService<TContext> : IServiceBase<TContext, ConfiguracaoCteTransportador>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
