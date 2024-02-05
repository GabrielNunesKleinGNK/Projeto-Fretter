using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IImportacaoArquivoTipoItemApplication<TContext> : IApplicationBase<TContext, ImportacaoArquivoTipoItem>
        where TContext : IUnitOfWork<TContext>
    {

    }
}