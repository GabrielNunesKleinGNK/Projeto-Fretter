using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace Fretter.Repository.Repositories
{
    public class FaturaArquivoRepository<TContext> : RepositoryBase<TContext, FaturaArquivo>, IFaturaArquivoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public FaturaArquivoRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

        public int GravarFaturaArquivo(DataTable faturaArquivo, DataTable criticas)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FaturaArquivo", faturaArquivo)
                {
                    TypeName = "Fretter.Tp_FaturaArquivo",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@Criticas", criticas)
                {
                    TypeName = "Fretter.Tp_FaturaArquivoCritica",
                    SqlDbType = SqlDbType.Structured,
                },
            };

            return ExecuteStoredProcedureWithParamScalar<int>("[Fretter].[SetFaturaArquivo]", parameters);
        }
    }
}
