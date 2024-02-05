using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IServiceBase<TContext, TEntity>
      where TContext : IUnitOfWork<TContext>
    {
        TEntity Save(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int chave);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true);
    }
}
