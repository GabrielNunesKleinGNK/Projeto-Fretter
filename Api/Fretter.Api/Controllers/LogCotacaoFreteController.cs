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
    [Route("api/logCotacaoFrete")]
    [ApiController]
    public class LogCotacaoFreteController : ControllerBase<FretterContext, Fatura, FaturaViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly ILogElasticSearchService _service;

        public LogCotacaoFreteController(ILogElasticSearchService service,
            IFaturaApplication<FretterContext> application,
            IUsuarioHelper user) : base(application)
        {
            _user = user;
            _service = service;
        }

        [HttpPost("lista")]
        public async Task<IActionResult> GetLista(LogCotacaoFreteFiltro filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            return Ok(await _service.GetLogCotacaoFreteLista(filtro));
        }
    }
}
