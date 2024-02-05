using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EmpresaConfigService<TContext> : ServiceBase<TContext, EmpresaConfig>, IEmpresaConfigService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaConfigRepository<TContext> _Repository;

        public EmpresaConfigService(IEmpresaConfigRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
