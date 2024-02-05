using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Domain.Services
{
    public class ServiceBase<TContexto, TEntity> : IServiceBase<TContexto, TEntity>
            where TEntity : EntityBase
            where TContexto : IUnitOfWork<TContexto>
    {
        protected readonly IRepositoryBase<TContexto, TEntity> _repository;
        protected readonly IUnitOfWork<TContexto> _unitOfWork;
        protected readonly IUsuarioHelper _user;

        public ServiceBase(IRepositoryBase<TContexto, TEntity> repository, IUnitOfWork<TContexto> unitOfWork, IUsuarioHelper user)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._user = user;
        }

        public virtual TEntity Save(TEntity entidade)
        {
            //entidade.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
            entidade.AtualizarDataCriacao();
            entidade.Validate();
            _repository.Save(entidade);

            return entidade;
        }

        public virtual TEntity Update(TEntity entidade)
        {
            entidade.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            entidade.AtualizarDataAlteracao();
            entidade.Validate();
            _repository.Update(entidade);

            return entidade;
        }

        public virtual void Delete(int chave)
        {
            TEntity entity = _repository.Get(chave);
            entity.Inativar();
            entity.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            entity.AtualizarDataAlteracao();
            entity.Validate();
            _repository.Delete(entity);
        }

        public virtual TEntity Get(int id) => _repository.Get(id);
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) => _repository.GetAll(predicate);
        public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
        {
            //Filtro empresaId
            var empresaId = _user.UsuarioLogado.EmpresaId;
            filter.AddFilter(nameof(empresaId), empresaId);

            return _repository.GetPaginated(filter, start, limit, orderByDescending);
        }
        public IQueryable<TEntity> GetQuaryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
