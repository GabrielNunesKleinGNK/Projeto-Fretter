using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using Fretter.Domain.Dto.Relatorio;
using Fretter.Domain.Dto.Dashboard;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IDashboardService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        List<DashboardResumo> GetDashboardResumo(DashboardFiltro filtro);
        List<DashboardEntregasGrafico> GetDashboadEntregasGrafico(DashboardFiltro filtro);
        List<DashboardTransportadorValor> GetDashboadTransportadorValor(DashboardFiltro filtro);
        List<DashboardTransportadorQuantidade> GetDashboadTransportadorQuantidade(DashboardFiltro filtro);
        List<DashboardTransportadorTotal> GetDashboadTransportadorLista(DashboardFiltro filtro);
        byte[] GetDashboadTransportadorListaDownload(DashboardFiltro filtro);
    }
}	
