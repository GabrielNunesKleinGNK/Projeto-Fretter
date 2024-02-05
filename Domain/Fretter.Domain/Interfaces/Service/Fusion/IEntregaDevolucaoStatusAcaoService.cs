using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoStatusAcaoService<TContext> : IServiceBase<TContext, EntregaDevolucaoStatusAcao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
