using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class ConciliacaoTransportadorRepository<TContext> : RepositoryBase<TContext, EntregaConciliacao>, IConciliacaoTransportadorRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Conciliacao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Conciliacao>();

        public ConciliacaoTransportadorRepository(IUnitOfWork<TContext> context) : base(context) { }
        public int ProcessaConciliacaoTransportador(DataTable ocorrencias)
        {
            var watch = new Stopwatch();
            watch.Start();

            SqlParameter[] parameters =
            {
                new SqlParameter("@Itens", ocorrencias)
                {
                    TypeName = "Fretter.Tp_EntregaConciliacao",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            return ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaConciliacaoTransportador", parameters, null);            
        }
    }
}
