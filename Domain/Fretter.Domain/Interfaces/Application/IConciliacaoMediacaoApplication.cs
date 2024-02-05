using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IConciliacaoMediacaoApplication<TContext> : IApplicationBase<TContext, ConciliacaoMediacao>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	