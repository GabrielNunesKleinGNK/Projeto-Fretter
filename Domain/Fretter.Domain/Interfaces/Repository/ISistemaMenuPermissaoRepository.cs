using  Fretter.Domain.Entities;
using System.Collections.Generic;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface ISistemaMenuPermissaoRepository<TContext> : IRepositoryBase<TContext, SistemaMenuPermissao> 
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<SistemaMenu> GetMenus(int usuarioId);
    }
}	
	
