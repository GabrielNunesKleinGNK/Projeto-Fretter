using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class EmpresaIntegracaoItemDetalheApplication<TContext> : ApplicationBase<TContext, EmpresaIntegracaoItemDetalhe>, IEmpresaIntegracaoItemDetalheApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaIntegracaoItemDetalheService<TContext> _service;
        public EmpresaIntegracaoItemDetalheApplication(IUnitOfWork<TContext> context, IEmpresaIntegracaoItemDetalheService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public bool ReprocessarLote(List<long> ids)
        {
            return _service.ReprocessarLote(ids);
        }

        public EmpresaIntegracaoItemDetalhe ObterStatusReprocessamento(int id)
        {
            return _service.ObterStatusReprocessamento(id);
        }

        public List<EmpresaIntegracaoItemDetalheDto> ObterDados(EmpresaIntegracaoItemDetalheFiltro filtro)
        {
            var retorno = _service.ObterDados(filtro);
            _unitOfWork.Commit();

            return retorno;
        }
    }
}

