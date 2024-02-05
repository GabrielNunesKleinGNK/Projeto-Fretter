using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoStatusService<TContext> : ServiceBase<TContext, EntregaDevolucaoStatus>, IEntregaDevolucaoStatusService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaDevolucaoStatusRepository<TContext> _Repository;

        public EntregaDevolucaoStatusService(IEntregaDevolucaoStatusRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
