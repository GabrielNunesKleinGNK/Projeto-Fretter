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
    public class EmpresaTransporteConfiguracaoItemRepository<TContext> : RepositoryBase<TContext, EmpresaTransporteConfiguracaoItem>, IEmpresaTransporteConfiguracaoItemRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaTransporteConfiguracaoItem> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaTransporteConfiguracaoItem>();

        public EmpresaTransporteConfiguracaoItemRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
