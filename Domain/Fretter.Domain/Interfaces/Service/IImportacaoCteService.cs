using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoCteService<TContext> : IServiceBase<TContext, ImportacaoCte>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
