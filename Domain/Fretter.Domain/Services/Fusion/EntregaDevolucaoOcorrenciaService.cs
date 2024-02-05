using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoOcorrenciaService<TContext> : ServiceBase<TContext, EntregaDevolucaoOcorrencia>, IEntregaDevolucaoOcorrenciaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaDevolucaoOcorrenciaRepository<TContext> _Repository;

        public EntregaDevolucaoOcorrenciaService(IEntregaDevolucaoOcorrenciaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
