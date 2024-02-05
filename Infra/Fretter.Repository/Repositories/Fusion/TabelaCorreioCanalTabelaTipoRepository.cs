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
    public class TabelaCorreioCanalTabelaTipoRepository<TContext> : RepositoryBase<TContext, TabelaCorreioCanalTabelaTipo>, ITabelaCorreioCanalTabelaTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<TabelaCorreioCanalTabelaTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TabelaCorreioCanalTabelaTipo>();

        public TabelaCorreioCanalTabelaTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
