using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICanalVendaApplication<TContext> : IApplicationBase<TContext, CanalVenda>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	