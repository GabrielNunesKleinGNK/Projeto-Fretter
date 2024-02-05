using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class CanalVendaRepository<TContext> : RepositoryBase<TContext, CanalVenda>, ICanalVendaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<CanalVenda> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<CanalVenda>();

        public CanalVendaRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IPagedList<CanalVenda> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<CanalVenda, object>>[] includes)
        {
            var result = DbSet.AsQueryable();

            if (includes != null)
                foreach (var include in includes)
                    result = result.Include(include);

            return GetPagedList(result, filter, start, limit, orderByDescending);
        }
    }
}	
