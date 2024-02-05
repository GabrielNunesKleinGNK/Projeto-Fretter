using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Repository.Repositories
{
    public class EntregaTransporteTipoRepository<TContext> : RepositoryBase<TContext, EntregaTransporteTipo>, IEntregaTransporteTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaTransporteTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaTransporteTipo>();

        public EntregaTransporteTipoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<EntregaTransporteTipo> BuscaTipoTransporteAtivo()
        {
            try
            {
                return _dbSet.Where(t => t.Ativo).Include(t => t.EntregaTransporteServico).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }
    }
}
