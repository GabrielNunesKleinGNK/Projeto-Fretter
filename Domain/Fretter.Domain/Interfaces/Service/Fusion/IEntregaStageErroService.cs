using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageErroService<TContext> : IServiceBase<TContext, EntregaStageErro>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
