using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaConfiguracaoTipoApplication<TContext> : IApplicationBase<TContext, EntregaConfiguracaoTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	