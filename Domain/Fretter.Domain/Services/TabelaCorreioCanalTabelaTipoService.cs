using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TabelaCorreioCanalTabelaTipoService<TContext> : ServiceBase<TContext, TabelaCorreioCanalTabelaTipo>, ITabelaCorreioCanalTabelaTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ITabelaCorreioCanalTabelaTipoRepository<TContext> _Repository;

        public TabelaCorreioCanalTabelaTipoService(ITabelaCorreioCanalTabelaTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
