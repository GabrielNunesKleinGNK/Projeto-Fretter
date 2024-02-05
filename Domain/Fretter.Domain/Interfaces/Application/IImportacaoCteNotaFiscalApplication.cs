using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoCteNotaFiscalApplication<TContext> : IApplicationBase<TContext, ImportacaoCteNotaFiscal>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	