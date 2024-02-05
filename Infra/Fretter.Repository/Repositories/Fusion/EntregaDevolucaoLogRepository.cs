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
    public class EntregaDevolucaoLogRepository<TContext> : RepositoryBase<TContext, EntregaDevolucaoLog>, IEntregaDevolucaoLogRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucaoLog> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucaoLog>();

        public EntregaDevolucaoLogRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
