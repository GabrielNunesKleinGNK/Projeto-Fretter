using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageDestinatarioApplication<TContext> : IApplicationBase<TContext, EntregaStageDestinatario>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	