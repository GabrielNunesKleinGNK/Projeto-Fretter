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
    public class ConciliacaoMediacaoRepository<TContext> : RepositoryBase<TContext, ConciliacaoMediacao>, IConciliacaoMediacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ConciliacaoMediacao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ConciliacaoMediacao>();

        public ConciliacaoMediacaoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
