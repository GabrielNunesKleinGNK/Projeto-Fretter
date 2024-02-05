using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaTransporteConfiguracaoService<TContext> : IServiceBase<TContext, EmpresaTransporteConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
        Task<EmpresaTransporteConfiguracao> TesteIntegracao(EmpresaTransporteConfiguracao dadosParaConsulta);
    }
}	
