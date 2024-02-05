using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageEntradaApplication<TContext> : IApplicationBase<TContext, EntregaStageEntrada>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	