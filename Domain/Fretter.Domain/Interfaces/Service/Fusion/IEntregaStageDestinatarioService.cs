using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageDestinatarioService<TContext> : IServiceBase<TContext, EntregaStageDestinatario>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
