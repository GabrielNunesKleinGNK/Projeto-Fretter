using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoConfiguracaoTipoService<TContext> : ServiceBase<TContext, ImportacaoConfiguracaoTipo>, IImportacaoConfiguracaoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoConfiguracaoTipoRepository<TContext> _Repository;

        public ImportacaoConfiguracaoTipoService(IImportacaoConfiguracaoTipoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
