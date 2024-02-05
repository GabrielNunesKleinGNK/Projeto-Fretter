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
    public class EmpresaTransporteTipoRepository<TContext> : RepositoryBase<TContext, EmpresaTransporteTipo>, IEmpresaTransporteTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaTransporteTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaTransporteTipo>();

        public EmpresaTransporteTipoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<EmpresaTransporteTipo> GetAll(Expression<Func<EmpresaTransporteTipo, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Where(x => x.Ativo)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(x => x.Ativo)
                    .Where(predicate)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
