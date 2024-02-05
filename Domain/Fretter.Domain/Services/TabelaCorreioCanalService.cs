using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TabelaCorreioCanalService<TContext> : ServiceBase<TContext, TabelaCorreioCanal>, ITabelaCorreioCanalService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ITabelaCorreioCanalRepository<TContext> _Repository;

        public TabelaCorreioCanalService(ITabelaCorreioCanalRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
