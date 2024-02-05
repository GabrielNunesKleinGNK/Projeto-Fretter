using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoArquivoCategoriaApplication<TContext> : IApplicationBase<TContext, ImportacaoArquivoCategoria>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	