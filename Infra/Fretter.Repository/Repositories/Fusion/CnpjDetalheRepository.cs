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
    public class CnpjDetalheRepository<TContext> : RepositoryBase<TContext, CnpjDetalhe>, ICnpjDetalheRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<CnpjDetalhe> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<CnpjDetalhe>();

        public CnpjDetalheRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
