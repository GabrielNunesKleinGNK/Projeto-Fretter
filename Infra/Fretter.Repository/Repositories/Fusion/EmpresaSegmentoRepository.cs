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
    public class EmpresaSegmentoRepository<TContext> : RepositoryBase<TContext, EmpresaSegmento>, IEmpresaSegmentoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaSegmento> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaSegmento>();

        public EmpresaSegmentoRepository(IUnitOfWork<TContext> context) : base(context) { }

    }
}	
