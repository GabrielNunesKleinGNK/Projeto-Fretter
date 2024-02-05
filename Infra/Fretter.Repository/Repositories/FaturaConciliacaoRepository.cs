using DocumentFormat.OpenXml.Spreadsheet;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Repository.Repositories
{
    public class FaturaConciliacaoRepository<TContext> : RepositoryBase<TContext, FaturaConciliacao>, IFaturaConciliacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public FaturaConciliacaoRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

        public List<FaturaConciliacaoIntegracaoDTO> GetAllFaturaConciliacaoIntegracao(int faturaId, int empresaId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FaturaId", faturaId)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@EmpresaId", empresaId)
                {
                    SqlDbType = SqlDbType.Int
                }
            };

            return ExecuteStoredProcedureWithParam<FaturaConciliacaoIntegracaoDTO>("[Fretter].[GetFaturaConciliacaoIntegracao]", parameters);
        }

        public JsonIntegracaoFaturaConciliacaoDTO GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@EmpresaIntegracaoItemDetalheId", empresaIntegracaoItemDetalheId)
                {
                    SqlDbType = SqlDbType.Int
                }
            };

            return ExecuteStoredProcedureWithParam<JsonIntegracaoFaturaConciliacaoDTO>("[Fretter].[GetJsonIntegracaoFaturaConciliacao]", parameters)?.FirstOrDefault();
        }

        public FaturaConciliacaoIntegracaoDTO ExisteAlgumaReincidencia(DataTable faturaConciliacaoIds)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@ListaFaturaConciliacaoId", faturaConciliacaoIds)
                {
                    TypeName = "dbo.Tp_BigInt",
                    SqlDbType = SqlDbType.Structured
                }
            };

            return ExecuteStoredProcedureWithParam<FaturaConciliacaoIntegracaoDTO>("[Fretter].[ValidarReincidenciaReenvioConciliacao]", parameters)
                ?.FirstOrDefault() ?? new FaturaConciliacaoIntegracaoDTO(); ;
        }

        public void InserirTabelaReenvio(DataTable conciliacoesReenvio)
        {
            SqlParameter[] parameters =
            {           
                new SqlParameter("@FaturaConciliacoesReenvio", conciliacoesReenvio)
                {
                    TypeName = "Fretter.Tp_FaturaConciliacaoReenvio",
                    SqlDbType = SqlDbType.Structured
                }
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("[Fretter].[SetFaturaConciliacaoReenvio]", parameters);
        }
    }
}
