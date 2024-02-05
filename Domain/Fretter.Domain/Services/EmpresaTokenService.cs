using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EmpresaTokenService<TContext> : ServiceBase<TContext, EmpresaToken>, IEmpresaTokenService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTokenRepository<TContext> _Repository;

        public EmpresaTokenService(IEmpresaTokenRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
