using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICanalVendaConfigService<TContext> : IServiceBase<TContext, CanalVendaConfig>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
