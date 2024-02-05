using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class FaturaHistoricoRepository<TContext> : RepositoryBase<TContext, FaturaHistorico>, IFaturaHistoricoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<FaturaHistorico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<FaturaHistorico>();

        public FaturaHistoricoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<FaturaHistorico> GetHistoricoDeFaturasPorEmpresa(int faturaId)
        {
            return _dbSet
                    .Include(e => e.FaturaStatus)
                    .Include(e => e.FaturaStatusAnterior)
                    //.Include(e => e.FaturaPeriodoId)
                    .Where(x => x.FaturaId == faturaId)
                    .ToList();
        }
    }
}	
