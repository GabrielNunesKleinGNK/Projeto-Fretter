using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Repository.Repositories
{
    public class EntregaDevolucaoAcaoRepository<TContext> : RepositoryBase<TContext, EntregaDevolucaoAcao>, IEntregaDevolucaoAcaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucaoAcao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucaoAcao>();

        public EntregaDevolucaoAcaoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
