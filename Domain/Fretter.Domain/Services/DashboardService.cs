using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Dto.Relatorio;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class DashboardService<TContext> : IDashboardService<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        private readonly IGenericRepository<TContext> _repository;
        private readonly IUsuarioHelper _user;

        public DashboardService(IGenericRepository<TContext> repository, IUsuarioHelper user)
        {
            _repository = repository;
            _user = user;
        }

        public List<DashboardResumo> GetDashboardResumo(DashboardFiltro filtro)
        {
            filtro.UsuarioId = _user.UsuarioLogado.Id;
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return _repository.ExecuteStoredProcedure<DashboardResumo, DashboardFiltro>(filtro, "[Fretter].[GetDashboardResumo]");
        }

        public List<DashboardEntregasGrafico> GetDashboadEntregasGrafico(DashboardFiltro filtro)
        {
            filtro.UsuarioId = _user.UsuarioLogado.Id;
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return _repository.ExecuteStoredProcedure<DashboardEntregasGrafico, DashboardFiltro>(filtro, "[Fretter].[GetDashboadEntregasGrafico]");
        }

        public List<DashboardTransportadorValor> GetDashboadTransportadorValor(DashboardFiltro filtro)
        {
            filtro.UsuarioId = _user.UsuarioLogado.Id;
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return _repository.ExecuteStoredProcedure<DashboardTransportadorValor, DashboardFiltro>(filtro, "[Fretter].[GetDashboardTransportadorValor]");
        }

        public List<DashboardTransportadorTotal> GetDashboadTransportadorLista(DashboardFiltro filtro)
        {
            filtro.UsuarioId = _user.UsuarioLogado.Id;
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return _repository.ExecuteStoredProcedure<DashboardTransportadorTotal, DashboardFiltro>(filtro, "[Fretter].[GetDashboardTransportadorTotal]");
        }

        public byte[] GetDashboadTransportadorListaDownload(DashboardFiltro filtro)
        {
            var listGenerica = new List<object>();
            var listTotal = GetDashboadTransportadorLista(filtro);

            listGenerica.AddRange(listTotal);

            return listGenerica.ConvertToXlsx("transportadores", true);
        }


        public List<DashboardTransportadorQuantidade> GetDashboadTransportadorQuantidade(DashboardFiltro filtro)
        {
            filtro.UsuarioId = _user.UsuarioLogado.Id;
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return _repository.ExecuteStoredProcedure<DashboardTransportadorQuantidade, DashboardFiltro>(filtro, "[Fretter].[GetDashboadTransportadorQuantidade]");
        }
    }
}
