using Fretter.Domain.Dto.PedidoPendenteBSeller;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class PedidoPendenteBSellerRepository<TContext> : RepositoryBase<TContext, PedidoPendenteBSeller>, IPedidoPendenteBSellerRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {

        private DbSet<PedidoPendenteBSeller> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<PedidoPendenteBSeller>();

        public PedidoPendenteBSellerRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<EntregaPedido> BuscarEntregaPedido(DataTable itemEntrega)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ListCdEntrega", itemEntrega)
                    {
                        TypeName = "Fretter.Tp_CdEntregaList",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<EntregaPedido>("[Fretter].[GetPedidoEntrega]", parameters);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaPedido - Erro: {e.Message}");
            }
            return null;
        }

        public void SalvarPedidoPendenteBSeller(DataTable listaPedidoPendenteBeSeller)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Itens", listaPedidoPendenteBeSeller)
                    {
                        TypeName = "Fretter.Tp_PedidoPendenteBSeller",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                ExecuteStoredProcedureWithParamNonQuery<int>("[Fretter].[ProcessarPedidoPendenteBSeller]", parameters, commandTimeout: 30000);
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Salvando Pedidos Pendentes BSeller - : {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Salvando Pedidos Pendentes BSeller - : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
            }

        }

        public List<EmpresaProcessamento> BuscarEmpresaProcessamento()
        {
            try
            {
                var retorno = ExecuteStoredProcedure<EmpresaProcessamento, string>(string.Empty, "[Fretter].[GetPedidoPendenteProcessamento]");
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EmpresaProcessamento - Erro: {e.Message}");
            }
            return null;
        }

        public List<PedidoStatus> BuscarPedidoStatus(int tipoStatus)
        {
            try
            {

                SqlParameter[] parameters =
                {
                    new SqlParameter("@PedidoPendenteIntegracaoId", tipoStatus)
                    {
                        SqlDbType = SqlDbType.VarChar,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<PedidoStatus>("[Fretter].[GetPedidoPendenteStatus]", parameters);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando PedidoStatus - Erro: {e.Message}");
            }
            return null;
        }
    }

}
