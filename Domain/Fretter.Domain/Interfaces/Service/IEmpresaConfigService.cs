using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaConfigService<TContext> : IServiceBase<TContext, EmpresaConfig>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
