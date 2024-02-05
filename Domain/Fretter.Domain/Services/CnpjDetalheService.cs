using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class CnpjDetalheService<TContext> : ServiceBase<TContext, CnpjDetalhe>, ICnpjDetalheService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ICnpjDetalheRepository<TContext> _Repository;

        public CnpjDetalheService(ICnpjDetalheRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
