using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    //[AllowAnonymous]
    [Authorize("Bearer")]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase//: ControllerBase<FretterContext, Empresa, EmpresaViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly IDashboardService<CommandContext> _service;

        public DashboardController(IDashboardService<CommandContext> service, IUsuarioHelper user)
        {
            _user = user;
            _service = service;
        }

        [HttpPost("resumo")]
        public Task<IActionResult> GetDashboadResumo(DashboardFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetDashboardResumo(filtro)));
        }

        [HttpPost("entregas")]
        public Task<IActionResult> GetDashboadEntregasGrafico(DashboardFiltro filtro)
        {
            var list = _service.GetDashboadEntregasGrafico(filtro);
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("transportadoresValor")]
        public Task<IActionResult> GetDashboadTransportadorValor(DashboardFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetDashboadTransportadorValor(filtro)));
        }

        [HttpPost("transportadoresLista")]
        public Task<IActionResult> GetDashboadTransportadorLista(DashboardFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetDashboadTransportadorLista(filtro)));
        }

        [HttpPost("transportadoresListaDownload")]
        public async Task<IActionResult> GetDashboadTransportadorListaDownload(DashboardFiltro filtro)
        {
            var bytes = _service.GetDashboadTransportadorListaDownload(filtro);
            string fileName = "Transportador_Demonstrativo.Xlsx";
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, fileName);
        }

        [HttpPost("transportadoresQuantidade")]
        public Task<IActionResult> GetDashboadTransportadorQuantidade(DashboardFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetDashboadTransportadorQuantidade(filtro)));
        }
    }
}
