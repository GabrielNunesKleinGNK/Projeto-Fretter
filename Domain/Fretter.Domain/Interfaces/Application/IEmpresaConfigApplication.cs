using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaConfigApplication<TContext> : IApplicationBase<TContext, EmpresaConfig>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	