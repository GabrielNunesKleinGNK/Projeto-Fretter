using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaRepository<TContext> : IRepositoryBase<TContext, Entrega>
        where TContext : IUnitOfWork<TContext>
    {
        int ObterEntregaPorCodigoPedido(string codigoPedido);
    }
}	





	
