using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EmpresaTransportadorConfigService<TContext> : ServiceBase<TContext, EmpresaTransportadorConfig>, IEmpresaTransportadorConfigService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTransportadorConfigRepository<TContext> _Repository;

        public EmpresaTransportadorConfigService(IEmpresaTransportadorConfigRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
