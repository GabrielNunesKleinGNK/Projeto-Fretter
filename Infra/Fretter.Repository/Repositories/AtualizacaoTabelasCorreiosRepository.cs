    using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Enum;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class AtualizacaoTabelasCorreiosRepository<TContext> : RepositoryBase<TContext, TabelasCorreiosArquivo>, IAtualizacaoTabelasCorreiosRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<TabelasCorreiosArquivo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TabelasCorreiosArquivo>();
        public AtualizacaoTabelasCorreiosRepository(IUnitOfWork<TContext> context) : base(context) 
        {

        }

        public bool ImportarDadosTabelasTemps(DataTable listCapitais, DataTable listLocais, DataTable listDivisas, DataTable listEstaduais, DataTable listInteriores, DataTable listMatrizes)
        {
            try
            {

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Capitais", listCapitais)
                    {
                        TypeName = "Tp_Importacao_MFC_Capitais",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@Locais", listLocais)
                    {
                        TypeName = "Tp_Importacao_MFC_Local",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@Divisas", listDivisas)
                    {
                        TypeName = "Tp_Importacao_MFC_Divisa",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@Estaduais", listEstaduais)
                    {
                        TypeName = "Tp_Importacao_MFC_Estadual",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@Interiores", listInteriores)
                    {
                        TypeName = "Tp_Importacao_MFC_Interior",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@Matrizes", listMatrizes)
                    {
                        TypeName = "Tp_Importacao_MFC_Matriz",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<dynamic>("dbo.Pr_MFC_ImportaDadosAtualizaoCorreios", parameters.ToArray(), null, 1800);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Importar Dados Correios - Erro: {e.Message}");
                throw e;
            }
        }
        public bool AtualizarTabelasFinais()
        {
            try
            {
                var retorno = ExecuteStoredProcedureWithParamNonQuery<int>("dbo.Pr_MFC_AtualizaTabelasCorreios", null, null, 1800);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Importar Dados Correios - Erro: {e.Message}");
                throw e;
            }
        }
        public override IPagedList<TabelasCorreiosArquivo> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TabelasCorreiosArquivo, object>>[] includes)
        {
            var includeList = new List<Expression<Func<TabelasCorreiosArquivo, object>>>()
            {
                x => x.TabelaArquivoStatus
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, includeList.ToArray());
        }
        public override IEnumerable<TabelasCorreiosArquivo> GetAll(Expression<Func<TabelasCorreiosArquivo, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Include(x => x.TabelaArquivoStatus)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(predicate)
                    .Include(x => x.TabelaArquivoStatus)
                    .AsNoTracking()
                    .ToList();
        }


        private void ImportarCapitais(DataTable listCapitais)
        {

        }
        private void ImportarLocais(DataTable listLocais)
        {

        }
        private void ImportarDivisas(DataTable listDivisas)
        {

        }
        private void ImportarEstaduais(DataTable listEstaduais)
        {

        }
        private void ImportarInteriores(DataTable listInteriores)
        {

        }
        private void ImportarMAtrizes(DataTable listMatrizes)
        {

        }
    }
}
