using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IConciliacaoStatusApplication<TContext> : IApplicationBase<TContext, ConciliacaoStatus>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	