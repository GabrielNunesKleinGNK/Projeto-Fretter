using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageDestinatarioService<TContext> : ServiceBase<TContext, EntregaStageDestinatario>, IEntregaStageDestinatarioService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageDestinatarioRepository<TContext> _Repository;

        public EntregaStageDestinatarioService(IEntregaStageDestinatarioRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
