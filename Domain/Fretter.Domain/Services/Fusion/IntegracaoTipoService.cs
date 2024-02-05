using Fretter.Domain.Dto.EmpresaIntegracao;
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
    public class IntegracaoTipoService<TContext> : ServiceBase<TContext, IntegracaoTipo>, IIntegracaoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IIntegracaoTipoRepository<TContext> _repository;
        public IntegracaoTipoService(
                IIntegracaoTipoRepository<TContext> repository,
                IUnitOfWork<TContext> unitOfWork,
                IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }
    }
}
