using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class UsuarioTipoService<TContext> : ServiceBase<TContext, UsuarioTipo>, IUsuarioTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioTipoRepository<TContext> _Repository;

        public UsuarioTipoService(IUsuarioTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
