using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using Fretter.Repository.Util;
using System.Data;
using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Interfaces;
using System.Linq.Expressions;
using System;

namespace Fretter.Repository.Repositories
{
    public class IntegracaoRepository<TContext> : RepositoryBase<TContext, Integracao>, IIntegracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Integracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Integracao>();

        public IntegracaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<DeParaEmpresaIntegracao> BuscaCamposDePara(string proc)
        {
            var list = ExecuteStoredProcedureWithParam<DeParaEmpresaIntegracao>(proc, null);
            return list;
        }

        public override IPagedList<Integracao> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<Integracao, object>>[] includes)
        {
            List<Expression<Func<Integracao, object>>> integracaoIncludes = new List<Expression<Func<Integracao, object>>>()
            {
                x => x.IntegracaoTipo
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, integracaoIncludes.ToArray());
        }
    }
}
