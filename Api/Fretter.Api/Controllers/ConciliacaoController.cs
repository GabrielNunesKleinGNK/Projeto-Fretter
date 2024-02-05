using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Dto.Fretter.Conciliacao;

namespace Fretter.Api.Controllers
{
    [Route("api/Conciliacao")]
    [ApiController]
    public class ConciliacaoController : ControllerBase<FretterContext, Conciliacao, ConciliacaoViewModel>
    {
        IConciliacaoApplication<FretterContext> _application;
        public ConciliacaoController(IConciliacaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("Relatorio/Detalhe")]
        public Task<IActionResult> GetRelatorioDetalhado([FromQuery] RelatorioDetalhadoFiltroDTO filtro)
        {
            List<RelatorioDetalhadoDTO> list = _application.ObterRelatorioDetalhado(filtro);
            return Task.FromResult<IActionResult>(Ok(list));
        }
        [HttpGet("Relatorio/DetalheArquivo")]
        public Task<IActionResult> GetRelatorioDetalhadoArquivo([FromQuery] RelatorioDetalhadoFiltroDTO filtro)
        {
            List<RelatorioDetalhadoArquivoDTO> list = _application.ObterRelatorioDetalhadoArquivo(filtro);
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("Status")]
        public Task<IActionResult> GetStatus()
        {
            List<ConciliacaoStatus> list = _application.ObterStatus();
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("Arquivo/Detalhe/{conciliacaoId}")]
        public Task<IActionResult> GetArquivo(int conciliacaoId)
        {
            var urlArquivo = _application.ObterArquivoPorConciliacao(conciliacaoId);
            return Task.FromResult<IActionResult>(Ok(urlArquivo));
        }

        [HttpPost("EnviarParaRecalculoFrete")]
        public Task<IActionResult> PostEnviarParaRecalculoFrete(List<int> listaConciliacoes)
        {
            _application.EnviarParaRecalculoFrete(listaConciliacoes);

            return Task.FromResult<IActionResult>(Ok());
        }
        [HttpPost("EnviarParaRecalculoFreteMassivo")]
        public Task<IActionResult> PostEnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro)
        {
            _application.EnviarParaRecalculoFreteMassivo(filtro);

            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
