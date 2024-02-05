using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConciliacaoHistoricoService<TContext> : IServiceBase<TContext, ConciliacaoHistorico>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
