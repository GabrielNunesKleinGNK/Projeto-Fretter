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
    public class EntregaStageDestinatarioRepository<TContext> : RepositoryBase<TContext, EntregaStageDestinatario>, IEntregaStageDestinatarioRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStageDestinatario> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStageDestinatario>();

        public EntregaStageDestinatarioRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
