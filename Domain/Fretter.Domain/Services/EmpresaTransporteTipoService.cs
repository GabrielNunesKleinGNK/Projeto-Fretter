using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EmpresaTransporteTipoService<TContext> : ServiceBase<TContext, EmpresaTransporteTipo>, IEmpresaTransporteTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTransporteTipoRepository<TContext> _Repository;

        public EmpresaTransporteTipoService(IEmpresaTransporteTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
