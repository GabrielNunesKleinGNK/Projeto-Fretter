using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaConfiguracaoTipoService<TContext> : ServiceBase<TContext, EntregaConfiguracaoTipo>, IEntregaConfiguracaoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaConfiguracaoTipoRepository<TContext> _Repository;

        public EntregaConfiguracaoTipoService(IEntregaConfiguracaoTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
