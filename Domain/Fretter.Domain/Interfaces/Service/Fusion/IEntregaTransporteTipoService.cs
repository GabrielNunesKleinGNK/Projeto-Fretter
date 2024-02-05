using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaTransporteTipoService<TContext> : IServiceBase<TContext, EntregaTransporteTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
