using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IConfiguracaoApplication<TContext> : IApplicationBase<TContext, Configuracao>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	