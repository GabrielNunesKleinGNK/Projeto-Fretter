using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICanalConfigApplication<TContext> : IApplicationBase<TContext, CanalConfig>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	