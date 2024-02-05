
using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class ContratoTransportadorHistoricoRepository<TContext> : RepositoryBase<TContext, ContratoTransportadorHistorico>, IContratoTransportadorHistoricoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private DbSet<ContratoTransportadorHistorico> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ContratoTransportadorHistorico>();

        public ContratoTransportadorHistoricoRepository(IUnitOfWork<TContext> context,
                                               IUsuarioHelper user) : base(context) {
            _user = user;
        }

        public override IPagedList<ContratoTransportadorHistorico> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<ContratoTransportadorHistorico, object>>[] includes)
        {
            var configuracoes = new List<Expression<Func<ContratoTransportadorHistorico, object>>>()
            {
                x => x.Transportador,
                x => x.TransportadorCnpj,
                x => x.TransportadorCnpjCobranca,
                x => x.FaturaCiclo,
                x => x.CadastroUsuario
            };
            return base.GetPaginated(filter, start, limit, orderByDescending, configuracoes.ToArray());
        }
    }
}	
