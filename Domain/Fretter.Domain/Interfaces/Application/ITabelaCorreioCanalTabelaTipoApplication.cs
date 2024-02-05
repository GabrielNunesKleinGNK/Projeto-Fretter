using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ITabelaCorreioCanalTabelaTipoApplication<TContext> : IApplicationBase<TContext, TabelaCorreioCanalTabelaTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	