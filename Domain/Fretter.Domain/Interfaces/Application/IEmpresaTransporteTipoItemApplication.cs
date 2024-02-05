using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IEmpresaTransporteTipoItemApplication<TContext> : IApplicationBase<TContext, EmpresaTransporteTipoItem>
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<EmpresaTransporteTipoItem> GetEmpresaTransporteItemPorTipo(int transporteTipoId);
    }
}
	