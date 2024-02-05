using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;

namespace Fretter.Repository.Repositories
{
    public class PedidoPendenteTransportadorRepository<TContext> : RepositoryBase<TContext, PedidoPendenteTransportador>, IPedidoPendenteTransportadorRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<PedidoPendenteTransportador> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<PedidoPendenteTransportador>();

        public PedidoPendenteTransportadorRepository(IUnitOfWork<TContext> context) : base(context) { }


        public void SalvarPedidoPendenteTransportador(DataTable listaPedidoPendenteTransportador)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Itens", listaPedidoPendenteTransportador)
                    {
                        TypeName = "Fretter.Tp_PedidoPendenteTransportador",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                ExecuteStoredProcedureWithParamNonQuery<int>("[Fretter].[ProcessarPedidoPendenteTransportador]", parameters);
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Salvando Pedidos Pendentes Transportador - : {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Salvando Pedidos Pendentes Transportador - : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
            }

        }
    }

}
