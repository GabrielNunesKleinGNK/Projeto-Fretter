using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoCteCargaService<TContext> : IServiceBase<TContext, ImportacaoCteCarga>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
