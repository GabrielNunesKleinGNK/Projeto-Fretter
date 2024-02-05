using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ImportacaoCteCargaService<TContext> : ServiceBase<TContext, ImportacaoCteCarga>, IImportacaoCteCargaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoCteCargaRepository<TContext> _Repository;

        public ImportacaoCteCargaService(IImportacaoCteCargaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
