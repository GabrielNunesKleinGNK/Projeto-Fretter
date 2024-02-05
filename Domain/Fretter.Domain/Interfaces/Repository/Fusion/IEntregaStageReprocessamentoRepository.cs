using  Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaStageReprocessamentoRepository<TContext> : IRepositoryBase<TContext, EntregaStageReprocessamento>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	





	
