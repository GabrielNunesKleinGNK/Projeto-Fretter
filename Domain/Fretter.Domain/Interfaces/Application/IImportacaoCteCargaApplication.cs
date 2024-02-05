using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IImportacaoCteCargaApplication<TContext> : IApplicationBase<TContext, ImportacaoCteCarga>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	