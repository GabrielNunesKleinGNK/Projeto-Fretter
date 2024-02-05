using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TabelaTipoService<TContext> : ServiceBase<TContext, TabelaTipo>, ITabelaTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ITabelaTipoRepository<TContext> _Repository;

        public TabelaTipoService(ITabelaTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
