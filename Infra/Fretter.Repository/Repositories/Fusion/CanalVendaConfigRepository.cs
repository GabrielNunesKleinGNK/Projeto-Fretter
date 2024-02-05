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
    public class CanalVendaConfigRepository<TContext> : RepositoryBase<TContext, CanalVendaConfig>, ICanalVendaConfigRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<CanalVendaConfig> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<CanalVendaConfig>();

        public CanalVendaConfigRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
