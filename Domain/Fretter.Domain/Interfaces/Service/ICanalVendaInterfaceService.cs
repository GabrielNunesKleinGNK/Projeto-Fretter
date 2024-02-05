using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICanalVendaInterfaceService<TContext> : IServiceBase<TContext, CanalVendaInterface>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
