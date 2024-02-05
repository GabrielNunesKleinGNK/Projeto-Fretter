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
    public class EntregaStageErroRepository<TContext> : RepositoryBase<TContext, EntregaStageErro>, IEntregaStageErroRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageErro> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageErro>();

        public EntregaStageErroRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
