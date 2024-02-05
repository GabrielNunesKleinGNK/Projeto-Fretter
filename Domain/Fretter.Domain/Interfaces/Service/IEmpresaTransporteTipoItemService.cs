using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTransporteTipoItemService<TContext> : IServiceBase<TContext, EmpresaTransporteTipoItem>
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<EmpresaTransporteTipoItem> GetEmpresaTransporteItemPorTipo(int transporteTipoId);
    }
}	
