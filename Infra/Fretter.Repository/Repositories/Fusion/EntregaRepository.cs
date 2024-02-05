using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class EntregaRepository<TContext> : RepositoryBase<TContext, Entrega>, IEntregaRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Entrega> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Entrega>();
        public EntregaRepository(IUnitOfWork<TContext> context) : base(context) { }
        public int ObterEntregaPorCodigoPedido(string codigoPedido)
        {
            int entrega = _dbSet.Where(x => x.CodigoPedido == codigoPedido).Select(x => x.Id).FirstOrDefault();
            return entrega;
        }
    }
}	
