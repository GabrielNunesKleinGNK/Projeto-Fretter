using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IIndicadorConciliacaoService<TContext> : IServiceBase<TContext, IndicadorConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaIndicadorConciliacao();
    }
}
