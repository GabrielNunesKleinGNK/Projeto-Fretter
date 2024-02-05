using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoArquivoTipoService<TContext> : ServiceBase<TContext, ImportacaoArquivoTipo>, IImportacaoArquivoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoArquivoTipoRepository<TContext> _Repository;

        public ImportacaoArquivoTipoService(IImportacaoArquivoTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
