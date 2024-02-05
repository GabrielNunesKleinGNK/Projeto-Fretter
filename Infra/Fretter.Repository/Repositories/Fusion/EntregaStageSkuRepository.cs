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
    public class EntregaStageSkuRepository<TContext> : RepositoryBase<TContext, EntregaStageSku>, IEntregaStageSkuRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageSku> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageSku>();

        public EntregaStageSkuRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	