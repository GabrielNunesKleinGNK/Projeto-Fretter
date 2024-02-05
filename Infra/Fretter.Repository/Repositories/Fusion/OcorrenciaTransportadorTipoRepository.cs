using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Repository.Repositories
{
    public class OcorrenciaTransportadorTipoRepository<TContext> : RepositoryBase<TContext, OcorrenciaTransportadorTipo>, IOcorrenciaTransportadorTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<OcorrenciaTransportadorTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<OcorrenciaTransportadorTipo>();

        public OcorrenciaTransportadorTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
