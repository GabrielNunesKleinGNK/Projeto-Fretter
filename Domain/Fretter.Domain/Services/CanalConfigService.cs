using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CanalConfigService<TContext> : ServiceBase<TContext, CanalConfig>, ICanalConfigService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ICanalConfigRepository<TContext> _Repository;

        public CanalConfigService(ICanalConfigRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
