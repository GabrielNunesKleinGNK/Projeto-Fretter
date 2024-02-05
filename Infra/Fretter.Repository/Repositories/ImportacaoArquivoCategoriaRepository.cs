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
    public class ImportacaoArquivoCategoriaRepository<TContext> : RepositoryBase<TContext, ImportacaoArquivoCategoria>, IImportacaoArquivoCategoriaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoArquivoCategoria> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoArquivoCategoria>();

        public ImportacaoArquivoCategoriaRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
