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
    public class EntregaConfiguracaoTipoRepository<TContext> : RepositoryBase<TContext, EntregaConfiguracaoTipo>, IEntregaConfiguracaoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaConfiguracaoTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaConfiguracaoTipo>();

        public EntregaConfiguracaoTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
