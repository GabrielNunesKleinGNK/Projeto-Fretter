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
    public class ImportacaoCteNotaFiscalRepository<TContext> : RepositoryBase<TContext, ImportacaoCteNotaFiscal>, IImportacaoCteNotaFiscalRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoCteNotaFiscal> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoCteNotaFiscal>();

        public ImportacaoCteNotaFiscalRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
