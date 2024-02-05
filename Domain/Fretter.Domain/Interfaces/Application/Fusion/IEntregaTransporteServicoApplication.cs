using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaTransporteServicoApplication<TContext> : IApplicationBase<TContext, EntregaTransporteServico>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	