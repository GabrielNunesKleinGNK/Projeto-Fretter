using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoOcorrenciaService<TContext> : IServiceBase<TContext, EntregaDevolucaoOcorrencia>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
