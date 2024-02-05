using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaDevolucaoStatusAcaoRepository<TContext> : IRepositoryBase<TContext, EntregaDevolucaoStatusAcao> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
