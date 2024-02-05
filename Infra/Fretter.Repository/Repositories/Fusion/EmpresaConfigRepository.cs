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
    public class EmpresaConfigRepository<TContext> : RepositoryBase<TContext, EmpresaConfig>, IEmpresaConfigRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaConfig> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaConfig>();

        public EmpresaConfigRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
