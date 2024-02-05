using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageItemService<TContext> : ServiceBase<TContext, EntregaStageItem>, IEntregaStageItemService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageItemRepository<TContext> _Repository;

        public EntregaStageItemService(IEntregaStageItemRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
