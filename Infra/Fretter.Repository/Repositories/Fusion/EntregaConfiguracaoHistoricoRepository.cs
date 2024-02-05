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
    public class EntregaConfiguracaoHistoricoRepository<TContext> : RepositoryBase<TContext, EntregaConfiguracaoHistorico>, IEntregaConfiguracaoHistoricoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaConfiguracaoHistorico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaConfiguracaoHistorico>();

        public EntregaConfiguracaoHistoricoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
