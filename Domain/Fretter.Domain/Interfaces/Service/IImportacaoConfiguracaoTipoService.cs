using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoConfiguracaoTipoService<TContext> : IServiceBase<TContext, ImportacaoConfiguracaoTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
