using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoArquivoCategoriaService<TContext> : ServiceBase<TContext, ImportacaoArquivoCategoria>, IImportacaoArquivoCategoriaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoArquivoCategoriaRepository<TContext> _Repository;

        public ImportacaoArquivoCategoriaService(IImportacaoArquivoCategoriaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
