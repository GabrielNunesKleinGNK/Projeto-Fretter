using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Dto.EntregaDevolucao;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaDevolucao")]
    [ApiController]
    public class EntregaDevolucaoController : ControllerBase<FretterContext, EntregaDevolucao, EntregaDevolucaoViewModel>
    {
        public new readonly IEntregaDevolucaoApplication<FretterContext> _application;

        public EntregaDevolucaoController(IEntregaDevolucaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpPost("Entregas")]
        public async Task<IActionResult> GetEntregasDevolucoes(EntregaDevolucaoFiltro filtro)
        {
            var list = _mapper.Map<List<EntregaDevolucaoViewModel>>(_application.GetEntregasDevolucoes(filtro));
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("Acao")]
        public async Task<IActionResult> ExecutarAcao(EntregaDevolucaoAcaoDto acao)
        {
            _application.RealizarAcao(acao);
            return await Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost("Download")]
        public async Task<IActionResult> Download(List<EntregaDevolucaoDto> entregas)
        {
            var bytes = _application.Download(entregas);
            string fileName = "Entrega_Reversa.Xlsx";
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, fileName);
        }


    }
}
