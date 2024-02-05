using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaConfiguracaoHistoricoApplication<TContext> : IApplicationBase<TContext, EntregaConfiguracaoHistorico>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	