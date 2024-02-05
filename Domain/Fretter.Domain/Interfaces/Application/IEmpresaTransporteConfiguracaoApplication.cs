using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTransporteConfiguracaoApplication<TContext> : IApplicationBase<TContext, EmpresaTransporteConfiguracao>
		where TContext : IUnitOfWork<TContext>
	{
		Task<EmpresaTransporteConfiguracao> TesteIntegracao(EmpresaTransporteConfiguracao dadosParaConsulta);
	}
}
	