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
    public class ImportacaoArquivoStatusRepository<TContext> : RepositoryBase<TContext, ImportacaoArquivoStatus>, IImportacaoArquivoStatusRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoArquivoStatus> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoArquivoStatus>();

        public ImportacaoArquivoStatusRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
