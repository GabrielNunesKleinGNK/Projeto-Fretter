using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ISistemaMenuService<TContext> : IServiceBase<TContext, SistemaMenu>
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<SistemaMenu> GetMenusUsuarioLogado();
    }
}	
