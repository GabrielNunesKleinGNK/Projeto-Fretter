using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTokenService<TContext> : IServiceBase<TContext, EmpresaToken>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
