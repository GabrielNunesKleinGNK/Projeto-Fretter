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
    public class TabelaTipoCanalVendaRepository<TContext> : RepositoryBase<TContext, TabelaTipoCanalVenda>, ITabelaTipoCanalVendaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<TabelaTipoCanalVenda> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TabelaTipoCanalVenda>();

        public TabelaTipoCanalVendaRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
