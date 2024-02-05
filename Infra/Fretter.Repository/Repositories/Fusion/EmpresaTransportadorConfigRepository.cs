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
    public class EmpresaTransportadorConfigRepository<TContext> : RepositoryBase<TContext, EmpresaTransportadorConfig>, IEmpresaTransportadorConfigRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaTransportadorConfig> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaTransportadorConfig>();

        public EmpresaTransportadorConfigRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
