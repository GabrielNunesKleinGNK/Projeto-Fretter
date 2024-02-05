using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageRemetenteService<TContext> : IServiceBase<TContext, EntregaStageRemetente>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
