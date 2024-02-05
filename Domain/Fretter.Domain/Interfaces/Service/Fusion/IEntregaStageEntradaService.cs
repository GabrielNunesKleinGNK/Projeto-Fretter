using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageEntradaService<TContext> : IServiceBase<TContext, EntregaStageEntrada>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
