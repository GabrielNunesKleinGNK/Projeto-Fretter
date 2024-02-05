using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ITabelaCorreioCanalService<TContext> : IServiceBase<TContext, TabelaCorreioCanal>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
