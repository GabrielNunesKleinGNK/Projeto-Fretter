using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class SistemaMenuPermissaoRepository<TContext> : RepositoryBase<TContext, SistemaMenuPermissao>, ISistemaMenuPermissaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<SistemaMenuPermissao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<SistemaMenuPermissao>();

        public SistemaMenuPermissaoRepository(IUnitOfWork<TContext> context) : base(context) { }



        public IEnumerable<SistemaMenu> GetMenus(int usuarioId)
        {
            return _dbSet
                    .IgnoreQueryFilters()
                    .Include(o => o.Usuario)
                    .Include(o => o.SistemaMenu)
                    .Where(p => p.UsuarioId == usuarioId && p.Ativo)
                    .OrderBy(o => o.SistemaMenu.Ordem)
                    .Select(o => o.SistemaMenu);
        }
    }
}	
