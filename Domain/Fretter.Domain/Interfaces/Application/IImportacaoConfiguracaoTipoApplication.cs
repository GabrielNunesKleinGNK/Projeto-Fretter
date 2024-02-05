using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoConfiguracaoTipoApplication<TContext> : IApplicationBase<TContext, ImportacaoConfiguracaoTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	