using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ITabelaTipoApplication<TContext> : IApplicationBase<TContext, TabelaTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	