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
    public class MicroServicoRepository<TContext> : RepositoryBase<TContext, MicroServico>, IMicroServicoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<MicroServico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<MicroServico>();

        public MicroServicoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public MicroServico ObterMicroServicoPorEmpresa(int empresaId, int correioTransportadorId)
        {
            return _dbSet
                .Include(e => e.EmpresaTransportadorConfig)
                .Where(t => t.EmpresaTransportadorConfig.EmpresaId == empresaId && t.EmpresaTransportadorConfig.TransportadorId == correioTransportadorId && t.ServicoCodigo == "SDX")
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();
        }

        public override IEnumerable<MicroServico> GetAll(Expression<Func<MicroServico, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .ToList();
        }

    }
}
