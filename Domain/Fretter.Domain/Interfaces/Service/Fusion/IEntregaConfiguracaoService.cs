using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaConfiguracaoService<TContext> : IServiceBase<TContext, EntregaConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
        Task ProcessaEntregaConfiguracaoAtivo();
        EntregaConfiguracao GetConfiguracoesPorIdTipo(int Id_Tipo);
		List<EntregaConfiguracao> GetListaConfiguracoesPorIdTipo(int Id_Tipo);
		Task ReprocessaEntregaMirakl();
    }
}	
