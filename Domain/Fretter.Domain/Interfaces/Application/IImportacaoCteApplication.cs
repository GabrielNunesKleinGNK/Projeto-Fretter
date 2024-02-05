using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoCteApplication<TContext> : IApplicationBase<TContext, ImportacaoCte>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	