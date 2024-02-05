using Fretter.Domain.Entities;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IContratoTransportadorRegraRepository<TContext> : IRepositoryBase<TContext, ContratoTransportadorRegra>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
