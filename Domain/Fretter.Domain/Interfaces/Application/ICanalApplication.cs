
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICanalApplication<TContext> : IApplicationBase<TContext, Canal>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	