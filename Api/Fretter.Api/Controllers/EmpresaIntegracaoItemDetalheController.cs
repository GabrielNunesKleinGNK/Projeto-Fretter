using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;

namespace Fretter.Api.Controllers
{
    [Route("api/EmpresaIntegracaoItemDetalhe")]
    [ApiController]
    public class EmpresaIntegracaoItemDetalheController : ControllerBase<FretterContext, EmpresaIntegracaoItemDetalhe, EmpresaIntegracaoItemDetalheViewModel>
    {
        IEmpresaIntegracaoItemDetalheApplication<FretterContext> _application;
        public EmpresaIntegracaoItemDetalheController(IEmpresaIntegracaoItemDetalheApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpPost("ReprocessarLote")]
        public Task<IActionResult> ReprocessarLote(List<long> ids )
        {
            return Task.FromResult<IActionResult>(Ok(_application.ReprocessarLote(ids)));
        }

        [HttpPost("ObterStatus")]
        public Task<IActionResult> ObterStatusReprocessamento(int id)
        {
            return Task.FromResult<IActionResult>(Ok(_mapper.Map<EmpresaIntegracaoItemDetalheViewModel>(_application.ObterStatusReprocessamento(id))));
        }

        [HttpPost("ObterDados")]
        public Task<IActionResult> ObterDados(EmpresaIntegracaoItemDetalheFiltro filtro)
        {
            var t = _application.ObterDados(filtro);
            return Task.FromResult<IActionResult>(Ok(t));
        }
    }
}
