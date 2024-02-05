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
    public class ImportacaoArquivoTipoItemRepository<TContext> : RepositoryBase<TContext, ImportacaoArquivoTipoItem>, IImportacaoArquivoTipoItemRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoArquivoTipoItem> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoArquivoTipoItem>();

        public ImportacaoArquivoTipoItemRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override ImportacaoArquivoTipoItem Get(int id)
        {
            return DbSet.Include(t => t.ConciliacaoTipo).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public override IEnumerable<ImportacaoArquivoTipoItem> GetAll(Expression<Func<ImportacaoArquivoTipoItem, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Include(x => x.ConciliacaoTipo)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(predicate)
                    .Include(x => x.ConciliacaoTipo)
                    .AsNoTracking()
                    .ToList();
        }
    }
}	
