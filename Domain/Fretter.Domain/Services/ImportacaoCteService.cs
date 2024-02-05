using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoCteService<TContext> : ServiceBase<TContext, ImportacaoCte>, IImportacaoCteService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoCteRepository<TContext> _Repository;

        public ImportacaoCteService(IImportacaoCteRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
