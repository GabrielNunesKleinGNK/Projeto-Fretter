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
    public class EntregaTransporteServicoRepository<TContext> : RepositoryBase<TContext, EntregaTransporteServico>, IEntregaTransporteServicoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaTransporteServico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaTransporteServico>();

        public EntregaTransporteServicoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
