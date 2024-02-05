using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Controllers
{
    //[AllowAnonymous]
    [Authorize("Bearer")]
    [Route("api/logDashboard")]
    [ApiController]
    public class LogDashboardController : ControllerBase<FretterContext, Fatura, FaturaViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly ILogElasticSearchService _service;

        public LogDashboardController(ILogElasticSearchService service,
            IFaturaApplication<FretterContext> application,
            IUsuarioHelper user) : base(application)
        {
            _user = user;
            _service = service;
        }

        [HttpPost("lista")]
        public async Task<IActionResult> GetLista(LogDashboardFiltro filtro)
        {
            return Ok(await _service.GetLogDashboardLista(filtro));
        }

        [HttpPost("diario")]
        public async Task<IActionResult> GetLogDiario(LogDashboardFiltro filtro)
        {
            return Ok(await _service.GetLogDashboardDiario(filtro));
        }

        [HttpPost("resumoMensagem")]
        public async Task<IActionResult> GetLogMensagem(LogDashboardFiltro filtro)
        {
            return Ok(await _service.GetLogDashboardResumo(filtro));
        }

        [HttpPost("resumoProcesso")]
        public async Task<IActionResult> GetLogProcesso(LogDashboardFiltro filtro)
        {
            return Ok(await _service.GetLogDashboardProcesso(filtro));
        }

    }
}
