using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Interfaces;

namespace Fretter.Repository.Repositories
{
    public class EmpresaIntegracaoItemRepository<TContext> : RepositoryBase<TContext, EmpresaIntegracaoItem>, IEmpresaIntegracaoItemRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaIntegracaoItem> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaIntegracaoItem>();

        public EmpresaIntegracaoItemRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}
