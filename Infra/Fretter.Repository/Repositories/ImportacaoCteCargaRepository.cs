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
    public class ImportacaoCteCargaRepository<TContext> : RepositoryBase<TContext, ImportacaoCteCarga>, IImportacaoCteCargaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoCteCarga> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoCteCarga>();

        public ImportacaoCteCargaRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
