using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaTransporteTipoService<TContext> : ServiceBase<TContext, EntregaTransporteTipo>, IEntregaTransporteTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaTransporteTipoRepository<TContext> _Repository;

        public EntregaTransporteTipoService(IEntregaTransporteTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
