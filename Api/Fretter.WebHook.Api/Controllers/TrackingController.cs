using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces.Service.Webhook;
using Fretter.Repository.Contexts;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Dto.Webhook.Tracking.Entrada;
using System.Net.Http;
using System.Net;

namespace Fretter.WebHook.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly ITrackingService<FretterContext> _trackingService;

        public TrackingController(IUsuarioHelper usuarioHelper,
                                   ITrackingService<FretterContext> trackingService)
        {
            _usuarioHelper = usuarioHelper;
            _trackingService = trackingService;
        }

        [HttpPost("padrao")]
        public async Task<RetornoWs<ECriticaArquivo>> PostPadrao([FromBody] List<EntradaTracking> listaOcorrencias)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();
            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            var retorno = await _trackingService.TrackingPadraoAsync(listaOcorrencias, requestString, token);
            return retorno;
        }

        [HttpPost("especifico")]
        public async Task<ResponseEspecifico> PostEspecifico([FromBody] EntradaTrackingEspecifico tracking)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();
            Guid.TryParse(Request.Headers["logistic-provider-api-key"].FirstOrDefault().ToString(), out Guid token);
            var retorno = await _trackingService.TrackingEspecificoAsync(tracking, requestString, token);
            return retorno;
        }

        [HttpPost("sequoia")]
        public async Task<ResponseSequoia> PostSequoia([FromBody] List<EntradaTrackingSequoia> listaOcorrencias)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();
            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            var retorno = await _trackingService.TrackingSequoiaAsync(listaOcorrencias, requestString, token);
            return retorno;
        }

        [HttpPost("allpost")]
        public async Task<IActionResult> PostAllPost([FromBody] EntradaTrackingAllPost tracking)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            var tokenString = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer", "").TrimStart().TrimEnd();

            Guid.TryParse(tokenString, out Guid token);

            var retorno = await _trackingService.TrackingAllPostAsync(tracking, requestString, token);

            if (retorno.retorno.ToLower() == "sucesso")
                return Ok(retorno);
            else
                return BadRequest(retorno);
        }

        [HttpPost("freterapido")]
        public async Task<ResponseEspecifico> PostFreteRapido([FromBody] EntradaTrackingFreteRapido tracking)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();
            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            var retorno = await _trackingService.TrackingFreteRapidoAsync(tracking, requestString, token);
            return retorno;
        }

        [HttpPost("/api/webhook/intelipost")]
        public async Task<IActionResult> PostWebhookIntelipost([FromBody] EntradaSync entrada)
        {
            string requestString;
            Guid token = new Guid();

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(requestString))
            {
                requestString = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
            }

            try
            {
                var retorno = await _trackingService.WebhookSyncAsync(entrada, requestString, token);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/api/webhook/sync")]
        public async Task<IActionResult> PostWebhookSync([FromBody] EntradaSync entrada)
        {
            string requestString;
            Guid token = new Guid();

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(requestString))
            {
                requestString = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
            }

            try
            {
                var retorno = await _trackingService.WebhookSyncAsync(entrada, requestString, token);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("euentrego/{token}")]
        public async Task<IActionResult> PostEuEntrego([FromRoute] string token, [FromBody] EntradaTrackingEuEntrego tracking)
        {
            try
            {
                string requestString;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                    requestString = await reader.ReadToEndAsync();
                Guid tokenParse;

                if (string.IsNullOrEmpty(token))
                    Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out tokenParse);
                else Guid.TryParse(token, out tokenParse);

                var retorno = await _trackingService.TrackingEuEntregoAsync(tracking, requestString, tokenParse);

                if (retorno.errors?.Count() > 0)
                    return BadRequest(retorno);
                else return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("loggi")]
        public async Task<IActionResult> PostLoggi([FromBody] EntradaTrackingLoggi entrada)
        {
            Guid token;
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            var autBasic = Request.Headers["Authorization"].FirstOrDefault();
            var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(autBasic.Replace("Basic", "").Trim()));
            Guid.TryParse(tokenString.Substring(tokenString.IndexOf(":") + 1), out token);

            try
            {
                var retorno = await _trackingService.TrackingLoggiAsync(entrada, requestString, token);

                if (retorno.errors?.Count() > 0)
                    return BadRequest(retorno.errors);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
