using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class EntregaDevolucaoStatusAcaoRepository<TContext> : RepositoryBase<TContext, EntregaDevolucaoStatusAcao>, IEntregaDevolucaoStatusAcaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucaoStatusAcao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucaoStatusAcao>();

        public EntregaDevolucaoStatusAcaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<EntregaDevolucaoStatusAcao> GetAll(Expression<Func<EntregaDevolucaoStatusAcao, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .Include(x => x.Acao)
                .ToList();
        }
    }
}	
