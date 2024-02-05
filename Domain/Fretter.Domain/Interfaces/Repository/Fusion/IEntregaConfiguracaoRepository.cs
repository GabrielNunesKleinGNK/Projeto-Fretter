using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaConfiguracaoRepository<TContext> : IRepositoryBase<TContext, EntregaConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaConfiguracao GetConfiguracoesPorIdTipo(int Id_Tipo);
		public List<EntregaConfiguracao> GetListaConfiguracoesPorIdTipo(int Id_Tipo);
	}
}

