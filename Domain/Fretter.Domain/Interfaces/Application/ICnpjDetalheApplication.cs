using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ICnpjDetalheApplication<TContext> : IApplicationBase<TContext, CnpjDetalhe>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	