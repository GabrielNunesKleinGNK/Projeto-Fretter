using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTokenTipoService<TContext> : IServiceBase<TContext, EmpresaTokenTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
