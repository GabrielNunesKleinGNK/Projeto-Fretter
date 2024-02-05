using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using Fretter.Domain.Dto.Dashboard;
using System.Threading.Tasks;
using Fretter.Domain.Dto.LogElasticSearch;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ILogElasticSearchService
    {

        Task<List<LogDashboardLista>> GetLogDashboardLista(LogDashboardFiltro logDashboardFiltro);
        Task<List<LogDashboardConsolidado>> GetLogDashboardResumo(LogDashboardFiltro logDashboardFiltro);
        Task<List<LogDashboardConsolidado>> GetLogDashboardProcesso(LogDashboardFiltro logDashboardFiltro);
        Task<List<LogDashboardDiario>> GetLogDashboardDiario(LogDashboardFiltro logDashboardFiltro);
        Task<List<LogCotacaoFreteLista>> GetLogCotacaoFreteLista(LogCotacaoFreteFiltro logCotacaoFrete);
    }
}	
