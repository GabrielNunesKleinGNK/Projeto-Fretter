using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities.Fusion.SKU;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IGrupoApplication<TContext> : IApplicationBase<TContext, Grupo>
		where TContext : IUnitOfWork<TContext>
	{
		List<Grupo> GetGruposPorEmpresa();
	}
}
