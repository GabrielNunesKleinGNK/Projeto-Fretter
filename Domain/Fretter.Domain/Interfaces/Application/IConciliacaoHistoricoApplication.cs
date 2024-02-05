using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IConciliacaoHistoricoApplication<TContext> : IApplicationBase<TContext, ConciliacaoHistorico>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	