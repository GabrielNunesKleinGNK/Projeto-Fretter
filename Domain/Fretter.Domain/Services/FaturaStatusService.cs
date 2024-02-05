using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class FaturaStatusService<TContext> : ServiceBase<TContext, FaturaStatus>, IFaturaStatusService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IFaturaStatusRepository<TContext> _Repository;

        public FaturaStatusService(IFaturaStatusRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}
