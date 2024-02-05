
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IAnymarketApplication<TContext> : IApplicationBase<TContext, EntregaConfiguracao>
		where TContext : IUnitOfWork<TContext>
	{
		Task ImportarSku();
	}
}
	