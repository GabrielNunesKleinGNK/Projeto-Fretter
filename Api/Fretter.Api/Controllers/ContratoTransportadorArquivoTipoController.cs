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

namespace Fretter.Api.Controllers
{
    [Route("api/ContratoTransportadorArquivoTipoController")]
    [ApiController]
    public class ContratoTransportadorArquivoTipoController : ControllerBase<FretterContext, ContratoTransportadorArquivoTipo, ContratoTransportadorArquivoTipoViewModel>
    {
        public new readonly IContratoTransportadorArquivoTipoApplication<FretterContext> _application;
        public ContratoTransportadorArquivoTipoController(IContratoTransportadorArquivoTipoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpPost("ArquivoTipo")]
        public virtual Task<IActionResult> Post([FromBody] List<ContratoTransportadorArquivoTipoViewModel> model)
        {
            var result = _application.Save(_mapper.Map<List<ContratoTransportadorArquivoTipo>>(model));
            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
