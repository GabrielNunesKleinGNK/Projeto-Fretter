using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IUsuarioTipoService<TContext> : IServiceBase<TContext, UsuarioTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
