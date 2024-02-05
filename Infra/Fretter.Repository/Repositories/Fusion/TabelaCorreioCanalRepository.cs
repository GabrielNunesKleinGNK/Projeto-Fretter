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
    public class TabelaCorreioCanalRepository<TContext> : RepositoryBase<TContext, TabelaCorreioCanal>, ITabelaCorreioCanalRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<TabelaCorreioCanal> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TabelaCorreioCanal>();

        public TabelaCorreioCanalRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
