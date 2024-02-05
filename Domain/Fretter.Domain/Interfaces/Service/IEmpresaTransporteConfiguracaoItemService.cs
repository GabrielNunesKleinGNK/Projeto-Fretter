using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTransporteConfiguracaoItemService<TContext> : IServiceBase<TContext, EmpresaTransporteConfiguracaoItem>
        where TContext : IUnitOfWork<TContext>
    {
        void SaveRange(List<EmpresaTransporteConfiguracaoItem> items);
    }
}	
