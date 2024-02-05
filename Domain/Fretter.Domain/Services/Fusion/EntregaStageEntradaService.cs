using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageEntradaService<TContext> : ServiceBase<TContext, EntregaStageEntrada>, IEntregaStageEntradaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageEntradaRepository<TContext> _Repository;

        public EntregaStageEntradaService(IEntregaStageEntradaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
