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
    public class TabelaRepository<TContext> : RepositoryBase<TContext, Tabela>, ITabelaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Tabela> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Tabela>();

        public TabelaRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
