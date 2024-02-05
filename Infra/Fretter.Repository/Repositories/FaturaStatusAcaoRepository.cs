using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class FaturaStatusAcaoRepository<TContext> : RepositoryBase<TContext, FaturaStatusAcao>, IFaturaStatusAcaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<FaturaStatusAcao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<FaturaStatusAcao>();

        public FaturaStatusAcaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<FaturaStatusAcao> GetAll(Expression<Func<FaturaStatusAcao, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .Include(x => x.FaturaAcao)
                .ToList();
        }
    }
}	
