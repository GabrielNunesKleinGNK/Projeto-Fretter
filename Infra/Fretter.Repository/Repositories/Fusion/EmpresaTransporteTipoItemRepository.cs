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
    public class EmpresaTransporteTipoItemRepository<TContext> : RepositoryBase<TContext, EmpresaTransporteTipoItem>, IEmpresaTransporteTipoItemRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaTransporteTipoItem> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaTransporteTipoItem>();

        public EmpresaTransporteTipoItemRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<EmpresaTransporteTipoItem> GetAll(Expression<Func<EmpresaTransporteTipoItem, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Where(T => T.Ativo)
                    .Include(x => x.EmpresaTransporteTipo)
                    .Include(x => x.EmpresaTransporteConfiguracoes.Where(x => x.Ativo))
                    .Include(x => x.Transportador)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(predicate)
                    .Where(T => T.Ativo)
                    .Include(x => x.EmpresaTransporteTipo)
                    .Include(x => x.EmpresaTransporteConfiguracoes.Where(x => x.Ativo))
                    .Include(x => x.Transportador)
                    .AsNoTracking()
                    .ToList();
        }
    }
}
