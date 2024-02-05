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
    public class ConciliacaoHistoricoRepository<TContext> : RepositoryBase<TContext, ConciliacaoHistorico>, IConciliacaoHistoricoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ConciliacaoHistorico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ConciliacaoHistorico>();

        public ConciliacaoHistoricoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
