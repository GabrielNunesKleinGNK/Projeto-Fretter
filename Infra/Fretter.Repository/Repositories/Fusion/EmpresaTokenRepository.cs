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
    public class EmpresaTokenRepository<TContext> : RepositoryBase<TContext, EmpresaToken>, IEmpresaTokenRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaToken> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaToken>();

        public EmpresaTokenRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
