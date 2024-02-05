using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaDevolucaoStatusAcaoApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoStatusAcao>
		where TContext : IUnitOfWork<TContext>
	{
	}
}
	