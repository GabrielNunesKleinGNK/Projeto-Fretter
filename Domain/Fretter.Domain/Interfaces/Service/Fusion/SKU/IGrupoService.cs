using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities.Fusion.SKU;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IGrupoService<TContext> : IServiceBase<TContext, Grupo>
        where TContext : IUnitOfWork<TContext>
    {
        List<Grupo> GetGruposPorEmpresa();
    }
}
