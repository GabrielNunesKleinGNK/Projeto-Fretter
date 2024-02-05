
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaConfiguracaoApplication<TContext> : IApplicationBase<TContext, EntregaConfiguracao>
		where TContext : IUnitOfWork<TContext>
	{
		Task ProcessaEntregaConfiguracaoAtivo();
		Task ReprocessaEntregaMirakl();
	}
}
	