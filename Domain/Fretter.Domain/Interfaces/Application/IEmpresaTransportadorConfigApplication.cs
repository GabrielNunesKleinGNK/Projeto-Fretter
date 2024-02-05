using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTransportadorConfigApplication<TContext> : IApplicationBase<TContext, EmpresaTransportadorConfig>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	