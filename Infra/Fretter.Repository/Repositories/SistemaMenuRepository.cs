using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Repository.Repositories
{
    public class SistemaMenuRepository<TContext> : RepositoryBase<TContext, SistemaMenu>, ISistemaMenuRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<SistemaMenu> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<SistemaMenu>();

        public SistemaMenuRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
