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
    public class ConciliacaoStatusRepository<TContext> : RepositoryBase<TContext, ConciliacaoStatus>, IConciliacaoStatusRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ConciliacaoStatus> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ConciliacaoStatus>();

        public ConciliacaoStatusRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
