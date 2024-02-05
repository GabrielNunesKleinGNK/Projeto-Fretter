using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTokenApplication<TContext> : IApplicationBase<TContext, EmpresaToken>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	