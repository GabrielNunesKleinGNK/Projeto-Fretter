using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaDevolucaoHistoricoApplication<TContext> : IApplicationBase<TContext, EntregaDevolucaoHistorico>
		where TContext : IUnitOfWork<TContext>
	{
		List<EntregaDevolucaoHistorico>ObterHistoricoEntregaDevolucao(int entregaDevolucaoId);
	}
}
	