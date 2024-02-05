using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaTransporteServicoService<TContext> : IServiceBase<TContext, EntregaTransporteServico>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
