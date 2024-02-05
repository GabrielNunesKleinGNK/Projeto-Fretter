using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IConfiguracaoCteTransportadorApplication<TContext> : IApplicationBase<TContext, ConfiguracaoCteTransportador>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	