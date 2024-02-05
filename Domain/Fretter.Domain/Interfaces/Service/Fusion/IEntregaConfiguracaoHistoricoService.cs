using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaConfiguracaoHistoricoService<TContext> : IServiceBase<TContext, EntregaConfiguracaoHistorico>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
