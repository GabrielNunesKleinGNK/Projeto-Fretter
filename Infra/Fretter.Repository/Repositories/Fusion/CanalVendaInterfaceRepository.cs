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
    public class CanalVendaInterfaceRepository<TContext> : RepositoryBase<TContext, CanalVendaInterface>, ICanalVendaInterfaceRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<CanalVendaInterface> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<CanalVendaInterface>();

        public CanalVendaInterfaceRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
