using System;
using System.Collections.Generic;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CanalService<TContext> : ServiceBase<TContext, Canal>, ICanalService<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        private readonly IRepositoryBase<TContext, Canal> _repository;
        private readonly IUsuarioHelper _user;

        public CanalService(IRepositoryBase<TContext, Canal> repository,
                            IUsuarioHelper user,
                            IUnitOfWork<TContext> unitOfWork)
            : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _user = user;
        }

        public List<Dto.Dashboard.Canal> GetCanalPorEmpresa()
        {
            var filtro = new Dto.Dashboard.Canal() { EmpresaId = _user.UsuarioLogado.EmpresaId };
            return _repository.ExecuteStoredProcedure<Dto.Dashboard.Canal, Dto.Dashboard.Canal>(filtro, "[Fretter].[GetCanaisPorEmpresa]");
        }
    }
}
