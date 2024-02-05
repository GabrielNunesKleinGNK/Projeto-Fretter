using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Fretter.Application
{
    public class ApplicationBase<TContext, TEntity> : IApplicationBase<TContext, TEntity>
       //where TEntity : EntityBase
       where TContext : IUnitOfWork<TContext>
    {
        protected readonly IServiceBase<TContext, TEntity> _service;
        protected readonly IUnitOfWork<TContext> _unitOfWork;

        public ApplicationBase(IUnitOfWork<TContext> context, IServiceBase<TContext, TEntity> service)
        {
            this._service = service;
            _unitOfWork = context;
        }


        public virtual TEntity Save(TEntity entity)
        {
            _service.Save(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _service.Update(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public virtual void Delete(int chave)
        {
            _service.Delete(chave);
            _unitOfWork.Commit();
        }

        public virtual TEntity Get(int id)
        {
            var entidade = _service.Get(id);
            return entidade;
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var t = _service.GetAll(predicate);
            return t;
        }

        public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true)
        {
            return _service.GetPaginated(filter, start, limit);
        }
    }

}
