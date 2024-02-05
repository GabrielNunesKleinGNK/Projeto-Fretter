using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaDevolucaoStatusAcaoApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoStatusAcao>, IEntregaDevolucaoStatusAcaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaDevolucaoStatusAcaoService<TContext> _service;
        public EntregaDevolucaoStatusAcaoApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoStatusAcaoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }
    }
}	
