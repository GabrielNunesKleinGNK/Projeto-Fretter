using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoArquivoStatusApplication<TContext> : IApplicationBase<TContext, ImportacaoArquivoStatus>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	