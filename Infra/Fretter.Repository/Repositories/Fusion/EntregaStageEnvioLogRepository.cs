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
    public class EntregaStageEnvioLogRepository<TContext> : RepositoryBase<TContext, EntregaStageEnvioLog>, IEntregaStageEnvioLogRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageEnvioLog> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageEnvioLog>();

        public EntregaStageEnvioLogRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
