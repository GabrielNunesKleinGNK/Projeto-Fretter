using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaDevolucaoAcaoRepository<TContext> : IRepositoryBase<TContext, EntregaDevolucaoAcao> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
