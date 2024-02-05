using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EmpresaTokenTipoService<TContext> : ServiceBase<TContext, EmpresaTokenTipo>, IEmpresaTokenTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTokenTipoRepository<TContext> _Repository;

        public EmpresaTokenTipoService(IEmpresaTokenTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
