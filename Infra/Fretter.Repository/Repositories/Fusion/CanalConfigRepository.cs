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
    public class CanalConfigRepository<TContext> : RepositoryBase<TContext, CanalConfig>, ICanalConfigRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<CanalConfig> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<CanalConfig>();
        public CanalConfigRepository(IUnitOfWork<TContext> context) : base(context) { }

    }
}	
