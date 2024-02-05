using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IContratoTransportadorHistoricoApplication<TContext> : IApplicationBase<TContext, ContratoTransportadorHistorico>
		where TContext : IUnitOfWork<TContext>
	{
	}
}
	