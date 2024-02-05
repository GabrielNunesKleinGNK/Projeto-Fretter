using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ConciliacaoHistoricoService<TContext> : ServiceBase<TContext, ConciliacaoHistorico>, IConciliacaoHistoricoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IConciliacaoHistoricoRepository<TContext> _Repository;

        public ConciliacaoHistoricoService(IConciliacaoHistoricoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
