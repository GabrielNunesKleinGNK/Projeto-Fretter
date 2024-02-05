using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConfiguracaoService<TContext> : IServiceBase<TContext, Configuracao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
