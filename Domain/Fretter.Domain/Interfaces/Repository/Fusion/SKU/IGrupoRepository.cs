
using Fretter.Domain.Entities.Fusion.SKU;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Repository.Fusion
{
    public interface IGrupoRepository<TContext> : IRepositoryBase<TContext, Grupo>
        where TContext : IUnitOfWork<TContext>
    {
        List<Grupo> GetGruposPorEmpresa();
    }
}
