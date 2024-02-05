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
    public class ImportacaoConfiguracaoTipoRepository<TContext> : RepositoryBase<TContext, ImportacaoConfiguracaoTipo>, IImportacaoConfiguracaoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoConfiguracaoTipo> _dbSet => ((FretterContext)UnitOfWork).Set<ImportacaoConfiguracaoTipo>();

        public ImportacaoConfiguracaoTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
