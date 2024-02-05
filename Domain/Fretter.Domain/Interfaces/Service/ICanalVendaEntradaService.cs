using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICanalVendaEntradaService<TContext> : IServiceBase<TContext, CanalVendaEntrada>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
