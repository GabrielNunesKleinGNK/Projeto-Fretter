using Fretter.Domain.Dto.Menu;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Application
{
    public interface ISisMenuApplication<TContext> : IApplicationBase<TContext, Domain.Entities.SisMenu>
		where TContext : IUnitOfWork<TContext>
	{
		UserMenu GetUserMenu();
	}
}
	