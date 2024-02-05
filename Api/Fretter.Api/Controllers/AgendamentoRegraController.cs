using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Api.Models.Fusion;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoRegraController : ControllerBase<FretterContext, AgendamentoRegra, AgendamentoRegraViewModel>
    {
        IAgendamentoRegraApplication<FretterContext> _application;
        public AgendamentoRegraController(IAgendamentoRegraApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("RegraTipoOperador")]
        public Task<IActionResult> GetRegraTipoOperador()
        {
            List<RegraTipoOperador> list = _application.ObtemRegraTiposOperadores();
            var result = _mapper.Map<List<RegraTipoOperadorViewModel>>(list);

            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpGet("RegraTipoItem")]
        public Task<IActionResult> GetRegraTipoItem()
        {
            List<RegraTipoItem> list = _application.ObtemRegraTipoItem();
            var result = _mapper.Map<List<RegraTipoItemViewModel>>(list);

            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpGet("RegraTipo")]
        public Task<IActionResult> GetRegraTipo()
        {
            List<RegraTipo> list = _application.ObtemRegraTipo();
            var result = _mapper.Map<List<RegraTipoViewModel>>(list);

            return Task.FromResult<IActionResult>(Ok(result));
        }


        

        [HttpPost("IncluirRegra")]
        public Task<IActionResult> IncluirRegra([FromBody] AgendamentoRegraViewModel agendamentoRegraViewModel)
        {
            int resultado = _application.GravaRegra(agendamentoRegraViewModel.Model());

            return Task.FromResult<IActionResult>(Ok(resultado));
        }

        [HttpPut("AlterarRegra")]
        public Task<IActionResult> AlterarRegra([FromBody] AgendamentoRegraViewModel agendamentoRegraViewModel)
        {
            int resultado = _application.GravaRegra(agendamentoRegraViewModel.Model());

            return Task.FromResult<IActionResult>(Ok(resultado));
        }

        [HttpPut("InativarRegra/{id}")]
        public Task<IActionResult> InativarRegra(int id)
        {
            int resultado = _application.InativarRegra(id);

            return Task.FromResult<IActionResult>(Ok(resultado));
        }
    }
}
