using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaConfiguracaoHistoricoService<TContext> : ServiceBase<TContext, EntregaConfiguracaoHistorico>, IEntregaConfiguracaoHistoricoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaConfiguracaoHistoricoRepository<TContext> _Repository;

        public EntregaConfiguracaoHistoricoService(IEntregaConfiguracaoHistoricoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
