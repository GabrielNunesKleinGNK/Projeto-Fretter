using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;

namespace Fretter.Application
{
    public class EntregaStageCallBackApplication<TContext> : ApplicationBase<TContext, EntregaStageCallBack>, IEntregaStageCallBackApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaStageCallBackService<TContext> _service;

        public EntregaStageCallBackApplication(IUnitOfWork<TContext> context, IEntregaStageCallBackService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public int ProcessaEntregaStageCallback()
        {
            int quantidade = _service.ProcessaCallbackEntregaStage().Result;
            return quantidade;
        }
    }
}
