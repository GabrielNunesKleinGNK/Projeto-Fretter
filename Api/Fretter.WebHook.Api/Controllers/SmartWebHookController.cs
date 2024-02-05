using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Interfaces.Mensageria;
using Fretter.Domain.Interfaces;

namespace Fretter.WebHook.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SmartWebHookController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public SmartWebHookController(IUsuarioHelper usuarioHelper,
                                      IRabbitMQPublisher rabbitMQPublisher)
        {
            _usuarioHelper = usuarioHelper;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MensagemProvedorRetornoDto retornoDto)
        {
            _rabbitMQPublisher.PublishMessage("MensagemProvedorRetorno.1", retornoDto);

            return Ok(_usuarioHelper.UsuarioLogado.Id);
        }
    }
}
