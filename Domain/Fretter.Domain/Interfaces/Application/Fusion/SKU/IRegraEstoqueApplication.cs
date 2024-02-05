using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities.Fusion.SKU;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IRegraEstoqueApplication<TContext> : IApplicationBase<TContext, RegraEstoque>
		where TContext : IUnitOfWork<TContext>
	{
	}
}
