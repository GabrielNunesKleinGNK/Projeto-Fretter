using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ConciliacaoMediacaoService<TContext> : ServiceBase<TContext, ConciliacaoMediacao>, IConciliacaoMediacaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IConciliacaoMediacaoRepository<TContext> _Repository;

        public ConciliacaoMediacaoService(IConciliacaoMediacaoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
