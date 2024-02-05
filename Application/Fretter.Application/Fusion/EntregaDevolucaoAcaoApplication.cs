using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaDevolucaoAcaoApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoAcao>, IEntregaDevolucaoAcaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaDevolucaoAcaoService<TContext> _service;
        public EntregaDevolucaoAcaoApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoAcaoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }
    }
}	
