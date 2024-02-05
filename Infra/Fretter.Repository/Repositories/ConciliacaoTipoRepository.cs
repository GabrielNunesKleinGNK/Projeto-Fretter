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
    public class ConciliacaoTipoRepository<TContext> : RepositoryBase<TContext, ConciliacaoTipo>, IConciliacaoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ConciliacaoTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ConciliacaoTipo>();

        public ConciliacaoTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
