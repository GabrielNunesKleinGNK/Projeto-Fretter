using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoEntregaController : ControllerBase<FretterContext, AgendamentoEntrega, AgendamentoEntregaViewModel>
    {
        IAgendamentoEntregaApplication<FretterContext> _application;

        public AgendamentoEntregaController(IAgendamentoEntregaApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("produto/{codigo}")]
        public IActionResult ObterProdutoPorCodigo(string codigo)
        {
            var result = _application.ObterProdutoPorCodigo(codigo);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("disponibilidade")]
        public IActionResult ObterDisponibilidade([FromBody] AgendamentoDisponibilidadeFiltro filtro)
        {
            var result = _application.ObterDisponibilidade(filtro);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> ConsultaCep(string cep)
        {
            var result = _application.ObterCep(cep);

            if (result == null)
                return await Task.FromResult<IActionResult>(NotFound());

            return await Task.FromResult<IActionResult>(Ok(result));
        }

        public override Task<IActionResult> Post([FromBody] AgendamentoEntregaViewModel model)
        {
            var res = _application.Save(_mapper.Map<AgendamentoEntrega>(model.Model()));
            return Task.FromResult<IActionResult>(Ok(res));
        }
    }
}
