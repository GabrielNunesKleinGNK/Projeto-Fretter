using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class EntregaDevolucaoHistoricoRepository<TContext> : RepositoryBase<TContext, EntregaDevolucaoHistorico>, IEntregaDevolucaoHistoricoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucaoHistorico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucaoHistorico>();

        public EntregaDevolucaoHistoricoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<EntregaDevolucaoHistorico> ObterHistoricoEntregaDevolucao(int entregaDevolucaoId)
        {
            return _dbSet
                    .Include(e => e.StatusAnterior)
                    .Include(e => e.StatusAtual)
                    .Where(x => x.EntregaDevolucaoId == entregaDevolucaoId)
                    .ToList();
        }
    }
}	
