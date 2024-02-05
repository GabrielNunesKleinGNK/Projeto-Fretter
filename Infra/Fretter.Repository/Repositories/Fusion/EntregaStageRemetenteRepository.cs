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
    public class EntregaStageRemetenteRepository<TContext> : RepositoryBase<TContext, EntregaStageRemetente>, IEntregaStageRemetenteRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageRemetente> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageRemetente>();

        public EntregaStageRemetenteRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
