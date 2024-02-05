
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Api.Models.Fusion;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/Grupo")]
    [ApiController]
    public class GrupoController : ControllerBase<FretterContext, Grupo, GrupoViewModel>
    {
        IGrupoApplication<FretterContext> _application;
        public GrupoController(IGrupoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("GruposPorEmpresa")]
        public Task<IActionResult> GetGruposPorEmpresa()
        {
            var list = _application.GetGruposPorEmpresa();
            var result = _mapper.Map<List<GrupoViewModel>>(list);
            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
