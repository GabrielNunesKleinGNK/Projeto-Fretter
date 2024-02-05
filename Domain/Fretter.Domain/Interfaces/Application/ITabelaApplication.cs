using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ITabelaApplication<TContext> : IApplicationBase<TContext, Tabela>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	