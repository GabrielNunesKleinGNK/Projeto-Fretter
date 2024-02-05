using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fretter.Domain.Services
{
    public class ImportacaoArquivoTipoItemService<TContext> : ServiceBase<TContext, ImportacaoArquivoTipoItem>, IImportacaoArquivoTipoItemService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoArquivoTipoItemRepository<TContext> _Repository;

        public ImportacaoArquivoTipoItemService(IImportacaoArquivoTipoItemRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public override IEnumerable<ImportacaoArquivoTipoItem> GetAll(Expression<Func<ImportacaoArquivoTipoItem, bool>> predicate = null)
        {            
            return _Repository.GetAll(predicate);
        }
    }
}	
