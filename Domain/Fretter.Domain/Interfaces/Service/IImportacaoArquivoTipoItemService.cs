using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoArquivoTipoItemService<TContext> : IServiceBase<TContext, ImportacaoArquivoTipoItem>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
