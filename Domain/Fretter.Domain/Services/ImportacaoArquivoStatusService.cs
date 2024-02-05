using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoArquivoStatusService<TContext> : ServiceBase<TContext, ImportacaoArquivoStatus>, IImportacaoArquivoStatusService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoArquivoStatusRepository<TContext> _Repository;

        public ImportacaoArquivoStatusService(IImportacaoArquivoStatusRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
