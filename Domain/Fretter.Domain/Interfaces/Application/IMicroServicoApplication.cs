using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IMicroServicoApplication<TContext> : IApplicationBase<TContext, MicroServico>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	