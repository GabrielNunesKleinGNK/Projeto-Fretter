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
using Fretter.Domain.Dto.Conciliacao;

namespace Fretter.Api.Controllers
{
    [Route("api/ContratoTransportador")]
    [ApiController]
    public class ContratoTransportadorController : ControllerBase<FretterContext, ContratoTransportador, ContratoTransportadorViewModel>
    {
        IContratoTransportadorApplication<FretterContext> _application;
        public ContratoTransportadorController(IContratoTransportadorApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }


        [HttpGet("ObterMicroServico")]
        public virtual Task<IActionResult> ObterMicroServico()
        {
            var result = _application.ObterMicroServicoPorEmpresa();
            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpGet("ObterOcorrencias")]
        public virtual Task<IActionResult> ObterOcorrencias()
        {
            var result = _application.ObterOcorrenciasPorEmpresa();
            return Task.FromResult<IActionResult>(Ok(result));
        }
        [HttpPost("Regras")]
        public virtual Task<IActionResult> SalvarContratoTransportadorRegra([FromBody] List<ContratoTransportadorRegraViewModel> model)
        {
            var result = _application.ProcessaContratoTransportadorRegra(_mapper.Map<List<ContratoTransportadorRegra>>(model));
            return Task.FromResult<IActionResult>(Ok(result));
        }
        [HttpPost("ObterRegras")]
        public virtual Task<IActionResult> ObterContratoTransportadorRegra([FromBody] ContratoTransportadorRegraFiltroDTO model)
        {
            var result = _mapper.Map<List<ContratoTransportadorRegraViewModel>>(_application.ObterContratoTransportadorRegra(model));
            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
