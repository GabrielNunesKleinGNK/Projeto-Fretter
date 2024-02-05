using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
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
    public class EmpresaTransporteConfiguracaoApplication<TContext> : ApplicationBase<TContext, EmpresaTransporteConfiguracao>, IEmpresaTransporteConfiguracaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaTransporteConfiguracaoService<TContext> _service;
        public EmpresaTransporteConfiguracaoApplication(IUnitOfWork<TContext> context, IEmpresaTransporteConfiguracaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public Task<EmpresaTransporteConfiguracao> TesteIntegracao(EmpresaTransporteConfiguracao dadosParaConsulta)
        {
            var response = _service.TesteIntegracao(dadosParaConsulta);
            _unitOfWork.Commit();
            return response;
        }
    }
}

