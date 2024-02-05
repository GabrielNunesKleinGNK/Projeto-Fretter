using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface ITipoRepository<TContext> : IRepositoryBase<TContext, Tipo>
        where TContext : IUnitOfWork<TContext>
    {
        new IEnumerable<Tipo> GetAll(Expression<Func<Tipo, bool>> predicate = null);
    }

}

