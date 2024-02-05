using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Exceptions;

namespace Fretter.Repository.Repositories
{
    public class ConfiguracaoCteTransportadorRepository<TContext> : RepositoryBase<TContext, ConfiguracaoCteTransportador>, IConfiguracaoCteTransportadorRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ConfiguracaoCteTransportador> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ConfiguracaoCteTransportador>();
        public ConfiguracaoCteTransportadorRepository(IUnitOfWork<TContext> context) : base(context) { }

        public override IEnumerable<ConfiguracaoCteTransportador> GetAll(Expression<Func<ConfiguracaoCteTransportador, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.TransportadorCnpj)
                .Include(x => x.ConfiguracaoCteTipo)
                .ToList();
        }
        public override IPagedList<ConfiguracaoCteTransportador> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<ConfiguracaoCteTransportador, object>>[] includes)
        {
            var configuracoes = new List<Expression<Func<ConfiguracaoCteTransportador, object>>>()
            {
                x => x.TransportadorCnpj,
                x => x.ConfiguracaoCteTipo
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, configuracoes.ToArray());
        }

        public override ConfiguracaoCteTransportador Save(ConfiguracaoCteTransportador entity)
        {
            var existeConfigDb = base.GetQueryable()
                .Include(x => x.ConfiguracaoCteTipo)
                .Include(x => x.TransportadorCnpj)
                .Where(x => x.Ativo == true && x.TransportadorCnpjId == entity.TransportadorCnpjId && x.ConfiguracaoCteTipoId == entity.ConfiguracaoCteTipoId && x.EmpresaId == entity.EmpresaId)
                .FirstOrDefault();

            if (existeConfigDb != null)
                throw new DomainException(nameof(ConfiguracaoCteTransportador), "Save", $"Já existe uma configuração para o Tipo: {existeConfigDb.ConfiguracaoCteTipo.Descricao} e Transportador CNPJ: {existeConfigDb.TransportadorCnpj.CNPJ}");

            return base.Save(entity);
        }
    }
}
