using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;

namespace Fretter.Application
{
    public class EntregaDevolucaoCallBackApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoLog>, IEntregaDevolucaoCallBackApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaDevolucaoCallBackService<TContext> _service;

        public EntregaDevolucaoCallBackApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoCallBackService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public int ProcessaEntregaDevolucaoCallback()
        {
            int quantidade = _service.ProcessaCallbackEntregaDevolucao().Result;
            return quantidade;
        }
    }
}
