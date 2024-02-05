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
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Dto.Webhook.Entrega.Entrada;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace Fretter.WebHook.Api.Controllers
{
    #region Entrega
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IEntregaService<FretterContext> _entregaService;
        private readonly TelemetryClient _telemetryClient;

        public EntregaController(IUsuarioHelper usuarioHelper,
                                 IEntregaService<FretterContext> entregaService,
                                 TelemetryClient telemetryClient)
        {
            _usuarioHelper = usuarioHelper;
            _entregaService = entregaService;
            _telemetryClient = telemetryClient;
        }

        [HttpPost("padrao")]
        public async Task<RetornoWs<EEntregaErro>> PostPadrao([FromBody] entradaEntrega entrada)
        {
            var watch = new Stopwatch();
            watch.Start();

            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(requestString))
            {
                requestString = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
            }

            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            List<Tuple<string, string>> headers = Request.Headers.Select(x => new Tuple<string, string>(x.Key, x.Value)).ToList();

            _telemetryClient.TrackEvent("EntregaPadrao");

            var retorno = await _entregaService.EntregaPadraoAsync(entrada, requestString, token, headers);
            _telemetryClient.GetMetric("EntregaPadrao:TempoDeResposta").TrackValue(watch.ElapsedMilliseconds);
            watch.Stop();

            return retorno;
        }

        [HttpPost("custom")]
        public async Task<RetornoWs<EEntregaErro>> PostCustom([FromBody] EntradaEntregaDirect entrada)
        {
            var retorno = await _entregaService.EntregaCustomAsync(entrada);
            return retorno;
        }
    }
    #endregion

    #region Entrega Custom
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaCustomController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IEntregaService<FretterContext> _entregaService;

        public EntregaCustomController(IUsuarioHelper usuarioHelper,
                                 IEntregaService<FretterContext> entregaService)
        {
            _usuarioHelper = usuarioHelper;
            _entregaService = entregaService;
        }

        [HttpPost("restoque")]
        public async Task<RetornoWs<EEntregaErro>> PostRestoque([FromBody] Shipment entrada)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(requestString))
            {
                requestString = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
            }

            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            List<Tuple<string, string>> headers = Request.Headers.Select(x => new Tuple<string, string>(x.Key, x.Value)).ToList();

            var retorno = await _entregaService.EntregaCustomRestoqueAsync(entrada, requestString, token, headers);
            return retorno;
        }

        [HttpPost("veste")]
        public async Task<RetornoWs<EEntregaErro>> PostVeste([FromBody] Shipment entrada)
        {
            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                requestString = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(requestString))
            {
                requestString = Newtonsoft.Json.JsonConvert.SerializeObject(entrada);
            }

            Guid.TryParse(Request.Headers["Token"].FirstOrDefault().ToString(), out Guid token);
            List<Tuple<string, string>> headers = Request.Headers.Select(x => new Tuple<string, string>(x.Key, x.Value)).ToList();

            var retorno = await _entregaService.EntregaCustomRestoqueAsync(entrada, requestString, token, headers);
            return retorno;
        }
    }
    #endregion
}
