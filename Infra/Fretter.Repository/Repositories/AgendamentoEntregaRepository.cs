using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Fretter.Repository.Repositories
{
    public class AgendamentoEntregaRepository<TContext> : RepositoryBase<TContext, AgendamentoEntrega>, IAgendamentoEntregaRepository<TContext> where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private DbSet<AgendamentoEntrega> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<AgendamentoEntrega>();

        public AgendamentoEntregaRepository(IUnitOfWork<TContext> context, IUsuarioHelper user) : base(context)
        {
            _user = user;
        }

        public override IPagedList<AgendamentoEntrega> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<AgendamentoEntrega, object>>[] includes)
        {
            var configuracoes = new List<Expression<Func<AgendamentoEntrega, object>>>()
            {
                x => x.Produtos,
                x => x.Destinatarios,
                x => x.Canal,
                x => x.MenuFreteRegiaoCepCapacidade
            };

          return  base.GetPaginated(filter, start, limit, orderByDescending, configuracoes.ToArray());
        } 
    }
}
