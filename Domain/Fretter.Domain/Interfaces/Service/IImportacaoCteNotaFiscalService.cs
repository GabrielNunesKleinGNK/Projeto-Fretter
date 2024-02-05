using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoCteNotaFiscalService<TContext> : IServiceBase<TContext, ImportacaoCteNotaFiscal>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
