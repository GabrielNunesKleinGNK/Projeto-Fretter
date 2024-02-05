using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaDevolucaoOcorrenciaApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoOcorrencia>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	