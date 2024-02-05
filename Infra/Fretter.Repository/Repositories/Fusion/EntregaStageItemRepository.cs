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
    public class EntregaStageItemRepository<TContext> : RepositoryBase<TContext, EntregaStageItem>, IEntregaStageItemRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageItem> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageItem>();

        public EntregaStageItemRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
