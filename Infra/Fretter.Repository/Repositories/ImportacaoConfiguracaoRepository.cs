using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Enum;

namespace Fretter.Repository.Repositories
{
    public class ImportacaoConfiguracaoRepository<TContext> : RepositoryBase<TContext, ImportacaoConfiguracao>, IImportacaoConfiguracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoConfiguracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoConfiguracao>();
        public ImportacaoConfiguracaoRepository(IUnitOfWork<TContext> context) : base(context)
        {
        }

        public override IEnumerable<ImportacaoConfiguracao> GetAll(Expression<Func<ImportacaoConfiguracao, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet
                    .Where(T => T.Ativo)
                    .Include(x => x.ArquivoTipo)
                    .Include(x => x.ConfiguracaoTipo)
                    .Include(x => x.Transportador)
                    .Include(x => x.Empresa)
                    .ToListAsync().Result;
            else
                return DbSet
                    .Where(predicate)
                    .Where(T => T.Ativo)
                    .Include(x => x.ArquivoTipo)
                    .Include(x => x.ConfiguracaoTipo)
                    .Include(x => x.Transportador)
                    .Include(x => x.Empresa)
                    .ToList();
        }
    }
}
