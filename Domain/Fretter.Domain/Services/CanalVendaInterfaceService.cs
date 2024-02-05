using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CanalVendaInterfaceService<TContext> : ServiceBase<TContext, CanalVendaInterface>, ICanalVendaInterfaceService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ICanalVendaInterfaceRepository<TContext> _Repository;

        public CanalVendaInterfaceService(ICanalVendaInterfaceRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
