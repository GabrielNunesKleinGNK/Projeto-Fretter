using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageEnvioLogService<TContext> : ServiceBase<TContext, EntregaStageEnvioLog>, IEntregaStageEnvioLogService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageEnvioLogRepository<TContext> _Repository;

        public EntregaStageEnvioLogService(IEntregaStageEnvioLogRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
