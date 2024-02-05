using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface ITabelaTipoCanalVendaApplication<TContext> : IApplicationBase<TContext, TabelaTipoCanalVenda>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	