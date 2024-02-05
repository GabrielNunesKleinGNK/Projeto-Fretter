using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Enum;
using Fretter.Domain.Config;
using Microsoft.Extensions.Options;
using Fretter.Domain.Dto.OcorrenciaArquivo;
using Fretter.Domain.Interfaces;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class OcorrenciaArquivoRepository<TContext> : RepositoryBase<TContext, OcorrenciaArquivo>, IOcorrenciaArquivoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private readonly TimeoutConfig _timeoutConfig;
        private DbSet<OcorrenciaArquivo> _dbSet => ((Contexts.CommandContext)UnitOfWork).Set<OcorrenciaArquivo>();

        public OcorrenciaArquivoRepository(IUnitOfWork<TContext> context,
            IOptions<TimeoutConfig> timeoutConfig) : base(context)
        {
            _timeoutConfig = timeoutConfig?.Value;
        }

        public List<OcorrenciaArquivoDto> GetOcorrenciaArquivoProcessamentos()
        {
            return ExecuteStoredProcedureWithParam<OcorrenciaArquivoDto>("dbo.GetOcorrenciaArquivoProcessamento", null);
        }

        public void AtualizarOcorrenciaArquivo(int idTabelArquivo, EnumTabelaArquivoStatus enumTabelaArquivoStatus,
            string objRetorno = null, int? qtAdvertencia = null, int? qtErros = null, int? qtRegistros = null, int? nrPercentualAtualizacao = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@IdTabelaArquivo", idTabelArquivo)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@IdTabelaArquivoStatus", (int)enumTabelaArquivoStatus)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Retorno", objRetorno ?? string.Empty)
                {
                    SqlDbType = SqlDbType.VarChar,
                    IsNullable = true
                },
                new SqlParameter("@QtAdvertencia", qtAdvertencia ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@QtErros", qtErros ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@QtRegistros", qtRegistros ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@NrPercentualAtualizacao", nrPercentualAtualizacao ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@DataAtualizacao", DateTime.Now)
                {
                    SqlDbType = SqlDbType.DateTime
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_Edi_AtualizarOcorrenciaArquivo", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);

        }

        public override IPagedList<OcorrenciaArquivo> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<OcorrenciaArquivo, object>>[] includes)
        {
            var result = DbSet.AsQueryable();

            if (includes != null)
                foreach (var include in includes)
                    result = result.Include(include);

            return GetPagedList(result, filter, start, limit, orderByDescending);
        }
    }
}
