using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICanalVendaConfigApplication<TContext> : IApplicationBase<TContext, CanalVendaConfig>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	