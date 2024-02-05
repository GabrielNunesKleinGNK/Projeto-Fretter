using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoCallBackService<TContext> : IServiceBase<TContext, EntregaDevolucaoLog>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessaCallbackEntregaDevolucao();
    }
}
