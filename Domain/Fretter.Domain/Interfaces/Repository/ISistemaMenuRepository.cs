using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface ISistemaMenuRepository<TContext> : IRepositoryBase<TContext, SistemaMenu> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
