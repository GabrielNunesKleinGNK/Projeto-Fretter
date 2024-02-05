using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaTransporteTipoApplication<TContext> : IApplicationBase<TContext, EntregaTransporteTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	