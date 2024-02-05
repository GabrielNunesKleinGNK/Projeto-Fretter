using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTokenTipoApplication<TContext> : IApplicationBase<TContext, EmpresaTokenTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	