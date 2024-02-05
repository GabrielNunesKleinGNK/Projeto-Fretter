using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageRemetenteApplication<TContext> : IApplicationBase<TContext, EntregaStageRemetente>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	