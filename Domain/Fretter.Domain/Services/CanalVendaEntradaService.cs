using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CanalVendaEntradaService<TContext> : ServiceBase<TContext, CanalVendaEntrada>, ICanalVendaEntradaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ICanalVendaEntradaRepository<TContext> _Repository;

        public CanalVendaEntradaService(ICanalVendaEntradaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
