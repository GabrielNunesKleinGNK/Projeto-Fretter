using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTransporteTipoApplication<TContext> : IApplicationBase<TContext, EmpresaTransporteTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	