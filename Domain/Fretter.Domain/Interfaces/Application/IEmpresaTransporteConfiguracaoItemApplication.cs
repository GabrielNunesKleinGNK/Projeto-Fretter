using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEmpresaTransporteConfiguracaoItemApplication<TContext> : IApplicationBase<TContext, EmpresaTransporteConfiguracaoItem>
		where TContext : IUnitOfWork<TContext>
	{
	}
}
	