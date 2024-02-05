using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoAcaoService<TContext> : IServiceBase<TContext, EntregaDevolucaoAcao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
