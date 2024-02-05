using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageSkuService<TContext> : ServiceBase<TContext, EntregaStageSku>, IEntregaStageSkuService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageSkuRepository<TContext> _Repository;

        public EntregaStageSkuService(IEntregaStageSkuRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
