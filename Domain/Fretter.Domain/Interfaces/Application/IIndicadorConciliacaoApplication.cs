using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IIndicadorConciliacaoApplication<TContext> : IApplicationBase<TContext, IndicadorConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaIndicadorConciliacao();
    }
}
	