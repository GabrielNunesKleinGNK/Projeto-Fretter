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
    public class ContratoTransportadorRepository<TContext> : RepositoryBase<TContext, ContratoTransportador>, IContratoTransportadorRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private DbSet<ContratoTransportador> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ContratoTransportador>();

        public ContratoTransportadorRepository(IUnitOfWork<TContext> context,
                                               IUsuarioHelper user) : base(context) {
            _user = user;
        }

        public override IPagedList<ContratoTransportador> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<ContratoTransportador, object>>[] includes)
        {
            var configuracoes = new List<Expression<Func<ContratoTransportador, object>>>()
            {
                x => x.Transportador,
                x => x.TransportadorCnpj,
                x => x.TransportadorCnpjCobranca,
                x => x.FaturaCiclo,
                x => x.CadastroUsuario,
                x => x.AlteracaoUsuario
            };
            return base.GetPaginated(filter, start, limit, orderByDescending, configuracoes.ToArray());
        }
    }
}	
