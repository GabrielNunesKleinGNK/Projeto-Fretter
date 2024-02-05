
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoArquivoTipoItemApplication<TContext> : ApplicationBase<TContext, ImportacaoArquivoTipoItem>, IImportacaoArquivoTipoItemApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoArquivoTipoItemService<TContext> _service;
        public ImportacaoArquivoTipoItemApplication(IUnitOfWork<TContext> context, IImportacaoArquivoTipoItemService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public override IEnumerable<ImportacaoArquivoTipoItem> GetAll(Expression<Func<ImportacaoArquivoTipoItem, bool>> predicate = null)
        {
            var t = _service.GetAll(predicate);
            return t;
        }
    }
}
