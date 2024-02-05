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
    public class TabelaTipoRepository<TContext> : RepositoryBase<TContext, TabelaTipo>, ITabelaTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<TabelaTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TabelaTipo>();

        public TabelaTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
