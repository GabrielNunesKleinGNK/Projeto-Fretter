using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageItemApplication<TContext> : IApplicationBase<TContext, EntregaStageItem>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	