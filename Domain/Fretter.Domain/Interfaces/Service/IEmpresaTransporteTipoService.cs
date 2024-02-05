using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTransporteTipoService<TContext> : IServiceBase<TContext, EmpresaTransporteTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
