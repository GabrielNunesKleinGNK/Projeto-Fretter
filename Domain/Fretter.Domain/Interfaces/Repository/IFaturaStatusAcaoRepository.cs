using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IFaturaStatusAcaoRepository<TContext> : IRepositoryBase<TContext, FaturaStatusAcao> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
