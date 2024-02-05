using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IEntregaDevolucaoCallBackApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoLog>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaEntregaDevolucaoCallback();
    }
}
