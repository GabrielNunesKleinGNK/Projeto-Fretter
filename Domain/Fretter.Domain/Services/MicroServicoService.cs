using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class MicroServicoService<TContext> : ServiceBase<TContext, MicroServico>, IMicroServicoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IMicroServicoRepository<TContext> _Repository;

        public MicroServicoService(IMicroServicoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
