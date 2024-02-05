using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IConciliacaoTipoApplication<TContext> : IApplicationBase<TContext, ConciliacaoTipo>
        where TContext : IUnitOfWork<TContext>
    {

    }
}
