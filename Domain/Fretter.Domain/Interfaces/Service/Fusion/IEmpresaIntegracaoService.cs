using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaIntegracaoService<TContext> : IServiceBase<TContext, EmpresaIntegracao>
        where TContext : IUnitOfWork<TContext>
    {
        EmpresaIntegracao Save(EmpresaIntegracao entity);
    }
}
