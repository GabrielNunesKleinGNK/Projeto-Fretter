using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaStageErroService<TContext> : ServiceBase<TContext, EntregaStageErro>, IEntregaStageErroService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageErroRepository<TContext> _Repository;

        public EntregaStageErroService(IEntregaStageErroRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
