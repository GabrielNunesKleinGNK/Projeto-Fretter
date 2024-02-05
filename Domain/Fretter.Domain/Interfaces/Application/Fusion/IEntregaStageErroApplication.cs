using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageErroApplication<TContext> : IApplicationBase<TContext, EntregaStageErro>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	