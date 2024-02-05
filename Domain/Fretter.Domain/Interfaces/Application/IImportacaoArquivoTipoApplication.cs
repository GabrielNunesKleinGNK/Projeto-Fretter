using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoArquivoTipoApplication<TContext> : IApplicationBase<TContext, ImportacaoArquivoTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	