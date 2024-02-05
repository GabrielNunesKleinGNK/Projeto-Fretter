using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
    public interface ISistemaMenuApplication<TContext> : IApplicationBase<TContext, SistemaMenu>
		where TContext : IUnitOfWork<TContext>
	{
		IEnumerable<SistemaMenu> GetMenusUsuarioLogado();
	}
}
	