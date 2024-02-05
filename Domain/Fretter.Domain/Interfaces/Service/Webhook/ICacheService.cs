using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fretter.Domain.Interfaces.Service.Webhook
{
    public interface ICacheService<TContext>
    {
        void InicializaCache();
        List<TEntity> ObterLista<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool apenasAtivos = true) where TEntity : EntityBase;
    }
}
