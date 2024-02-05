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
    public class EntregaStageEntradaRepository<TContext> : RepositoryBase<TContext, EntregaStageEntrada>, IEntregaStageEntradaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageEntrada> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageEntrada>();

        public EntregaStageEntradaRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
