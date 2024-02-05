using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using Fretter.Domain.Dto.ArquivoImportacaoLog;

namespace Fretter.Api.Controllers
{
    [Route("api/ArquivoImportacaoLog")]
    [ApiController]
    public class ArquivoImportacaoLogController : ControllerBase<FretterContext, ArquivoImportacao, ArquivoImportacaoViewModel>
    {
        public new readonly IArquivoImportacaoApplication<FretterContext> _application;

        public ArquivoImportacaoLogController(IArquivoImportacaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpPost("lista")]
        public async Task<IActionResult> GetListaLogArquivoImportacao(ArquivoImportacaoLogFiltro filtro)
        {
            return Ok(await _application.GetArquivoImportacaoLog(filtro));
        }
    }
}
