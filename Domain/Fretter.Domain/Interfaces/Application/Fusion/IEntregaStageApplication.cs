using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaStageApplication<TContext> : IApplicationBase<TContext, EntregaStage>
		where TContext : IUnitOfWork<TContext>
	{
		Task<int> PopulaFilaReclicagemEtiquetas();
	}
}
	