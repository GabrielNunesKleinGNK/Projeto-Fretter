using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaDevolucaoAcaoApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoAcao>
		where TContext : IUnitOfWork<TContext>
	{
	}
}
	