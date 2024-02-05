using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Repository.Repositories
{
    public class EntregaConfiguracaoRepository<TContext> : RepositoryBase<TContext, EntregaConfiguracao>, IEntregaConfiguracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaConfiguracao> _dbSet => _context.Set<EntregaConfiguracao>();

        public EntregaConfiguracaoRepository(IUnitOfWork<TContext> context) : base(context)
        {
            //_dbSet = ((DbContext)context).Set<EntregaConfiguracao>();
        }
        public override IEnumerable<EntregaConfiguracao> GetAll(Expression<Func<EntregaConfiguracao, bool>> predicate = null)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include( x=> x.Itens.Where(x => x.Ativo));
        }

        public EntregaConfiguracao GetConfiguracoesPorIdTipo(int Id_Tipo)
        {
            return _dbSet
                .Include(x => x.Itens.Where(x => x.Ativo))
                .FirstOrDefault(x => x.EntregaConfiguracaoTipo == Id_Tipo);
        }
        public List<EntregaConfiguracao> GetListaConfiguracoesPorIdTipo(int Id_Tipo)
		{
			return _dbSet
				.Include(x => x.Itens.Where(x => x.Ativo))
				.Where(x => x.EntregaConfiguracaoTipo == Id_Tipo).ToList();
		}
	}
}
