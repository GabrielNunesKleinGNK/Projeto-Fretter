using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageLogService<TContext> : ServiceBase<TContext, EntregaStageLog>, IEntregaStageLogService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageLogRepository<TContext> _Repository;

        public EntregaStageLogService(IEntregaStageLogRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
