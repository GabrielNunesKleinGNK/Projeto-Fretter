using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using Fretter.Repository.Util;
using System.Data;
using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Interfaces;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class EntregaOrigemImportacaoRepository<TContext> : RepositoryBase<TContext, EntregaOrigemImportacao>, IEntregaOrigemImportacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Integracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Integracao>();

        public EntregaOrigemImportacaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IPagedList<EntregaOrigemImportacao> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<EntregaOrigemImportacao, object>>[] includes)
        {
            var result = DbSet.AsQueryable();

            if (includes != null)
                foreach (var include in includes)
                    result = result.Include(include);

            return GetPagedList(result, filter, start, limit, orderByDescending);
        }
    }
}
