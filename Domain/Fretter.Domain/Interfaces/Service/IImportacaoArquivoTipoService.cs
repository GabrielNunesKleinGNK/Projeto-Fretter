using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoArquivoTipoService<TContext> : IServiceBase<TContext, ImportacaoArquivoTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
