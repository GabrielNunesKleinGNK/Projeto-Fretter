using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICanalVendaEntradaApplication<TContext> : IApplicationBase<TContext, CanalVendaEntrada>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	