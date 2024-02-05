using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaDevolucaoStatusApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoStatus>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	