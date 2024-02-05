using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Repository
{
    public class ContratoTransportadorRegraRepository<TContext> : RepositoryBase<TContext, ContratoTransportadorRegra>, IContratoTransportadorRegraRepository<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        public ContratoTransportadorRegraRepository(IUnitOfWork<TContext> context) : base(context)
        {
        }
    }
}
