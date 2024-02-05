using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageCallBackService<TContext> : IServiceBase<TContext, EntregaStageCallBack>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessaCallbackEntregaStage();
    }
}
