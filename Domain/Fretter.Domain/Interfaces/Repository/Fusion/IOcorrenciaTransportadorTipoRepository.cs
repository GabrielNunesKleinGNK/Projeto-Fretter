using Fretter.Domain.Entities;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IOcorrenciaTransportadorTipoRepository<TContext> : IRepositoryBase<TContext, OcorrenciaTransportadorTipo> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
