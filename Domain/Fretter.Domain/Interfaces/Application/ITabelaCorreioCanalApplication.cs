using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ITabelaCorreioCanalApplication<TContext> : IApplicationBase<TContext, TabelaCorreioCanal>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	