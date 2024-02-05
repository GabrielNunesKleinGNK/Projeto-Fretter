using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Repositories
{
    public class ConfiguracaoRepository<TContext> : RepositoryBase<TContext, Configuracao>, IConfiguracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Configuracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Configuracao>();

        public ConfiguracaoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
