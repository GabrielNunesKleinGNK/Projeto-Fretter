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
    public class ImportacaoArquivoTipoRepository<TContext> : RepositoryBase<TContext, ImportacaoArquivoTipo>, IImportacaoArquivoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoArquivoTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoArquivoTipo>();

        public ImportacaoArquivoTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
