using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageLogApplication<TContext> : IApplicationBase<TContext, EntregaStageLog>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	