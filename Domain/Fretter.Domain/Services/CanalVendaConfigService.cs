using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CanalVendaConfigService<TContext> : ServiceBase<TContext, CanalVendaConfig>, ICanalVendaConfigService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ICanalVendaConfigRepository<TContext> _Repository;

        public CanalVendaConfigService(ICanalVendaConfigRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
