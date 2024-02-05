
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ISisMenuService<TContext> : IServiceBase<TContext, SisMenu>
        where TContext : IUnitOfWork<TContext>
    {
        List<Dto.Menu.SisMenu> GetSisMenu();
    }
}
