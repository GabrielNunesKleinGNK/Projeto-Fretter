using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTransportadorConfigService<TContext> : IServiceBase<TContext, EmpresaTransportadorConfig>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
