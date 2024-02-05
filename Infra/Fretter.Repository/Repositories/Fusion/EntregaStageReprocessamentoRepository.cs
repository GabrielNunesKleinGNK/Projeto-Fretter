using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Fretter.Repository.Repositories
{
    public class EntregaStageReprocessamentoRepository<TContext> : RepositoryBase<TContext, EntregaStageReprocessamento>, IEntregaStageReprocessamentoRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Entrega> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Entrega>();
        public EntregaStageReprocessamentoRepository(IUnitOfWork<TContext> context) : base(context) { }
        public override IEnumerable<EntregaStageReprocessamento> GetAll(Expression<Func<EntregaStageReprocessamento, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .ToListAsync().Result;
            else
                return DbSet
                    .Where(predicate)
                    .ToList();
        }
    }
}	
