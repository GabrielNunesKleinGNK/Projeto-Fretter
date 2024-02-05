using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Interfaces;

namespace Fretter.Repository.Repositories
{
    public class EmpresaIntegracaoRepository<TContext> : RepositoryBase<TContext, EmpresaIntegracao>, IEmpresaIntegracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaIntegracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaIntegracao>();

        public EmpresaIntegracaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IPagedList<EmpresaIntegracao> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<EmpresaIntegracao, object>>[] includes)
        {
            List<Expression<Func<EmpresaIntegracao, object>>> empresaIntegracaoIncludes = new List<Expression<Func<EmpresaIntegracao, object>>>()
            {
                x => x.ListaIntegracoes,
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, empresaIntegracaoIncludes.ToArray());
        }
    }
}
