using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using Fretter.Repository.Util;
using System.Data;
using Fretter.Domain.Dto.EmpresaIntegracao;

namespace Fretter.Repository.Repositories
{
    public class IntegracaoTipoRepository<TContext> : RepositoryBase<TContext, IntegracaoTipo>, IIntegracaoTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<IntegracaoTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<IntegracaoTipo>();

        public IntegracaoTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}
