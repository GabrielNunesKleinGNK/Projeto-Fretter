using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service.Webhook;
using Fretter.Repository.Contexts;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Webhook.Tracking.Entrada;
using Fretter.Domain.Dto.Webhook.TrackingIntegracao;

namespace Fretter.WebHook.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingIntegracaoController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly ITrackingIntegracaoService<FretterContext> _trackingService;
        

        public TrackingIntegracaoController(IUsuarioHelper usuarioHelper,
                                  ITrackingIntegracaoService<FretterContext> trackingService)
        {
            _usuarioHelper = usuarioHelper;
            _trackingService = trackingService;
            
        }

        [HttpPost("sucesso")]
        public async Task<IActionResult> PostSucesso([FromBody] List<TrackingIntegracaoEntradaDto> entrada)
        {
            var hashProcessamento = Guid.NewGuid();
            Guid.TryParse(Request.Headers["Authorization"].FirstOrDefault()?.ToString(), out Guid token);

            var retorno = await _trackingService.ProcessaSucesso(entrada, hashProcessamento, token);

            return StatusCode(Convert.ToInt32(retorno.messages.FirstOrDefault().key), retorno);
        }

        [HttpPost("insucesso")]
        public async Task<IActionResult> PostInsucesso([FromBody] List<TrackingIntegracaoEntradaDto> entrada)
        {
            var hashProcessamento = Guid.NewGuid();
            Guid.TryParse(Request.Headers["Authorization"].FirstOrDefault().ToString(), out Guid token);

            var retorno = await _trackingService.ProcessaInsucesso(entrada, hashProcessamento, token);

            return StatusCode(Convert.ToInt32(retorno.messages.FirstOrDefault().key), retorno);
        }
    }
}
