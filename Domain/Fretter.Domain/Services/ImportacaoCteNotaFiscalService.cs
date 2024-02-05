using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoCteNotaFiscalService<TContext> : ServiceBase<TContext, ImportacaoCteNotaFiscal>, IImportacaoCteNotaFiscalService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoCteNotaFiscalRepository<TContext> _Repository;

        public ImportacaoCteNotaFiscalService(IImportacaoCteNotaFiscalRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
