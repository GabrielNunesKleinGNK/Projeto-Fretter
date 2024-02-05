using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EmpresaIntegracaoService<TContext> : ServiceBase<TContext, EmpresaIntegracao>, IEmpresaIntegracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEmpresaIntegracaoRepository<TContext> _repository;
        public EmpresaIntegracaoService(
                IEmpresaIntegracaoRepository<TContext> repository,
                IUnitOfWork<TContext> unitOfWork,
                IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }

        public override EmpresaIntegracao Save(EmpresaIntegracao entity)
        {
            entity.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            _repository.Save(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public override EmpresaIntegracao Update(EmpresaIntegracao entity)
        {
            entity.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            _repository.Update(entity);

            return entity;
        }
    }
}
