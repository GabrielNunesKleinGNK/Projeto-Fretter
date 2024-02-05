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
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class TipoRepository<TContext> : RepositoryBase<TContext, Tipo>, ITipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Tipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Tipo>();

        public TipoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<Tipo> GetAll(Expression<Func<Tipo, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet
                    .AsNoTracking()
                    .ToList();
            else
                return _dbSet
                    .Where(predicate)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
