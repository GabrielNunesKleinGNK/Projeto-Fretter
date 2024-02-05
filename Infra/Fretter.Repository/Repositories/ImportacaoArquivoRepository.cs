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
using Fretter.Domain.Dto.ImportacaoArquivo;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class ImportacaoArquivoRepository<TContext> : RepositoryBase<TContext, ImportacaoArquivo>, IImportacaoArquivoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoArquivo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoArquivo>();
        public ImportacaoArquivoRepository(IUnitOfWork<TContext> context) : base(context) 
        {

        }

        public List<ImportacaoArquivo> ObterArquivosPendentesConemb()
        {
            return  _dbSet.Where(x => x.ImportacaoArquivoStatusId == EnumImportacaoArquivoStatus.Pendente.GetHashCode() &&
                               x.ImportacaoArquivoTipoId == EnumImportacaoArquivoTipo.CONEMB.GetHashCode()).Take(50).ToList();
        }

        public List<ImportacaoArquivo> ObterArquivosPendentesCte()
        {
            return _dbSet.Where(x => x.ImportacaoArquivoStatusId == EnumImportacaoArquivoStatus.Pendente.GetHashCode() &&
                              x.ImportacaoArquivoTipoId == EnumImportacaoArquivoTipo.CTe.GetHashCode()).Take(50).ToList();
        }

        public List<ImportacaoArquivo> ObterArquivosPendentes()
        {
            return _dbSet.Where(x => x.ImportacaoArquivoStatusId == EnumImportacaoArquivoStatus.Pendente.GetHashCode()).Take(50).ToList();
        }

        public ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro)
        {
            SqlParameter[] parameters =
{
                new SqlParameter("@DataInicio", importacaoArquivoFiltro.StartDataCadastro)
                {
                    SqlDbType = SqlDbType.DateTime
                },
                new SqlParameter("@DataTermino", importacaoArquivoFiltro.EndDataCadastro)
                {
                    SqlDbType = SqlDbType.DateTime
                },
                new SqlParameter("@Nome", importacaoArquivoFiltro.Nome.Truncate(128))
                {
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter("@EmpresaId", importacaoArquivoFiltro.EmpresaId)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@TransportadorId", importacaoArquivoFiltro.TransportadorId)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@ImportacaoArquivoStatusId", importacaoArquivoFiltro.ImportacaoArquivoStatusId)
                {
                    SqlDbType = SqlDbType.Int
                }
            };

            return ExecuteStoredProcedureWithParam<ImportacaoArquivoResumo>("Fretter.GetImportacaoArquivoResumo", parameters).FirstOrDefault() ?? new ImportacaoArquivoResumo();
        }

        public override IEnumerable<ImportacaoArquivo> GetAll(Expression<Func<ImportacaoArquivo, bool>> predicate = null)
        {
            if(predicate == null)
                return DbSet
                   .Include(e => e.ImportacaoArquivoStatus)
                   .Include(e => e.ImportacaoArquivoTipo)
                   .Where(T => T.Ativo)
                   .ToList();
            else
                return DbSet
                       .Include(e => e.ImportacaoArquivoStatus)
                       .Include(e => e.ImportacaoArquivoTipo)
                       .Where(predicate)
                       .Where(T => T.Ativo)
                       .ToList();
        }

        public override IPagedList<ImportacaoArquivo> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<ImportacaoArquivo, object>>[] includes)
        {
            filter.Filters.RemoveAll(x => x.Value == null || x.Value.ToString() == string.Empty || x.Value.ToString() == "0");

            var includeList = new List<Expression<Func<ImportacaoArquivo, object>>>()
            {
                x => x.ImportacaoArquivoStatus,
                x => x.ImportacaoArquivoTipo,
                x => x.ImportacaoArquivoCriticas
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, includeList.ToArray());

        }
    }
}
