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
    public class EntregaDevolucaoStatusRepository<TContext> : RepositoryBase<TContext, EntregaDevolucaoStatus>, IEntregaDevolucaoStatusRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucaoStatus> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucaoStatus>();

        public EntregaDevolucaoStatusRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
