using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConciliacaoMediacaoService<TContext> : IServiceBase<TContext, ConciliacaoMediacao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
