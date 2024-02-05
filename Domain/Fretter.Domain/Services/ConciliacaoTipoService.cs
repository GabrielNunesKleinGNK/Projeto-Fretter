using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ConciliacaoTipoService<TContext> : ServiceBase<TContext, ConciliacaoTipo>, IConciliacaoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IConciliacaoTipoRepository<TContext> _Repository;

        public ConciliacaoTipoService(IConciliacaoTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
