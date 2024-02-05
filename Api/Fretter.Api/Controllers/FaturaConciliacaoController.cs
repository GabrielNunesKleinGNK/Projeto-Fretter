using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaConciliacaoController : ControllerBase<FretterContext, FaturaConciliacao, FaturaConciliacaoViewModel>
    {
        IFaturaConciliacaoApplication<FretterContext> _application;

        public FaturaConciliacaoController(IFaturaConciliacaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("GetAllFaturaConciliacaoIntegracao/{faturaId}")]
        public Task<IActionResult> GetAllFaturaConciliacaoIntegracao(int faturaId)
        {
            var result = _application.GetAllFaturaConciliacaoIntegracao(faturaId);

            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPost("ReenviarFaturaConciliacaoIndividual")]
        public Task<IActionResult> ReenviarFaturaConciliacaoIndividual([FromBody] FaturaConciliacaoReenvioDTO model)
        {
            var retorno = _application.ReenviarFaturaConciliacaoIndividual(model);

            return Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpPost("ReenviarFaturaConciliacaoMassivo")]
        public Task<IActionResult> ReenviarFaturaConciliacaoMassivo([FromBody] List<FaturaConciliacaoReenvioDTO> lstModel)
        {
            var retorno = _application.ReenviarFaturaConciliacaoMassivo(lstModel);

            return Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpGet("GetJsonIntegracaoFaturaConciliacao/{empresaIntegracaoItemDetalheId}")]
        public Task<IActionResult> GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId)
        {
            var retorno = _application.GetJsonIntegracaoFaturaConciliacao(empresaIntegracaoItemDetalheId);

            return Task.FromResult<IActionResult>(Ok(retorno));
        }
    }
}
