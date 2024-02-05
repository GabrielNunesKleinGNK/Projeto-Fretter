using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageSkuApplication<TContext> : IApplicationBase<TContext, EntregaStageSku>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	