using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Repositories
{
    public class IndicadorConciliacaoRepository<TContext> : RepositoryBase<TContext, IndicadorConciliacao>, IIndicadorConciliacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<IndicadorConciliacao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<IndicadorConciliacao>();

        public IndicadorConciliacaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public int ProcessaIndicadorConciliacao()
        {
            return ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaIndicadorConciliacao", null);
        }
    }
}
