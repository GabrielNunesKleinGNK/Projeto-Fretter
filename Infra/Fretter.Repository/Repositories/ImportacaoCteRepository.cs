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
    public class ImportacaoCteRepository<TContext> : RepositoryBase<TContext, ImportacaoCte>, IImportacaoCteRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ImportacaoCte> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ImportacaoCte>();

        public ImportacaoCteRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
