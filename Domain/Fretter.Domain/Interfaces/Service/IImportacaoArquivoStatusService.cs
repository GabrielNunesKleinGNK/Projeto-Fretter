using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoArquivoStatusService<TContext> : IServiceBase<TContext, ImportacaoArquivoStatus>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
