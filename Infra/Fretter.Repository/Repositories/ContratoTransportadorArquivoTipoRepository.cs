using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace Fretter.Repository.Repositories
{
    public class ContratoTransportadorArquivoTipoRepository<TContext> : RepositoryBase<TContext, ContratoTransportadorArquivoTipo>, IContratoTransportadorArquivoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private DbSet<ContratoTransportadorArquivoTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ContratoTransportadorArquivoTipo>();

        public ContratoTransportadorArquivoTipoRepository(IUnitOfWork<TContext> context,
                                               IUsuarioHelper user) : base(context)
        {
            _user = user;
        }

        public bool SaveRange(IEnumerable<ContratoTransportadorArquivoTipo> entities)
        {
            foreach (var contratoTransportadorArquivo in entities)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@ContratoTransportadorArquivoTipoId", contratoTransportadorArquivo.Id));
                parameters.Add(new SqlParameter("@EmpresaId", contratoTransportadorArquivo.EmpresaId));
                parameters.Add(new SqlParameter("@TransportadorId", contratoTransportadorArquivo.TransportadorId));
                parameters.Add(new SqlParameter("@ImportacaoArquivoTipoItemId", contratoTransportadorArquivo.ImportacaoArquivoTipoItemId));
                parameters.Add(new SqlParameter("@Alias", contratoTransportadorArquivo.Alias));
                parameters.Add(new SqlParameter("@UsuarioCadastro", _user.UsuarioLogado.Id));
                parameters.Add(new SqlParameter("@UsuarioAlteracao", _user.UsuarioLogado.Id));
                parameters.Add(new SqlParameter("@Ativo", contratoTransportadorArquivo.Ativo));

                var result = ExecuteStoredProcedureWithParam<int>("Fretter.SetContratoTransportadorArquivoTipo", parameters.ToArray()).FirstOrDefault();
            }

            return true;
        }

        public override IPagedList<ContratoTransportadorArquivoTipo> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<ContratoTransportadorArquivoTipo, object>>[] includes)
        {
            var includeList = new List<Expression<Func<ContratoTransportadorArquivoTipo, object>>>()
            {
                x => x.ImportacaoArquivoTipoItem,                
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, includeList.ToArray());
        }

        public override IEnumerable<ContratoTransportadorArquivoTipo> GetAll(Expression<Func<ContratoTransportadorArquivoTipo, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Where(T => T.Ativo)
                    .Include(T => T.ImportacaoArquivoTipoItem).ThenInclude(P => P.ConciliacaoTipo)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(predicate)
                    .Where(T => T.Ativo)
                    .Include(T => T.ImportacaoArquivoTipoItem).ThenInclude(P => P.ConciliacaoTipo)
                    .AsNoTracking()
                    .ToList();
        }


    }
}
