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
    public class CanalRepository<TContext> : RepositoryBase<TContext, Canal>, ICanalRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Canal> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Canal>();

        public CanalRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
