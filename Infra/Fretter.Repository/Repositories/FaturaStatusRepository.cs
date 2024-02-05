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
    public class FaturaStatusRepository<TContext> : RepositoryBase<TContext, FaturaStatus>, IFaturaStatusRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<FaturaStatus> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<FaturaStatus>();

        public FaturaStatusRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
