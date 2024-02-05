using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TabelaService<TContext> : ServiceBase<TContext, Tabela>, ITabelaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ITabelaRepository<TContext> _Repository;

        public TabelaService(ITabelaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
