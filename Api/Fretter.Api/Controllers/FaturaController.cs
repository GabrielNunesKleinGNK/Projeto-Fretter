using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Authorization;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Dto.Fatura;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;

namespace Fretter.Api.Controllers
{
    [Route("api/Fatura")]
    [ApiController]
    public class FaturaController : ControllerBase<FretterContext, Fatura, FaturaViewModel>
    {
        public new readonly IFaturaApplication<FretterContext> _application;
        public new readonly IApplicationBase<FretterContext, FaturaCiclo> _applicationFaturaCiclo;
        public new readonly IApplicationBase<FretterContext, ArquivoCobranca> _applicationArquivoCobranca;

        public FaturaController(IFaturaApplication<FretterContext> application,
                                IApplicationBase<FretterContext, FaturaCiclo> applicationFaturaCiclo,
                                IApplicationBase<FretterContext, ArquivoCobranca> applicationArquivoCobranca) : base(application)
        {
            _application = application;
            _applicationFaturaCiclo = applicationFaturaCiclo;
            _applicationArquivoCobranca = applicationArquivoCobranca;
        }

        [HttpPost("Faturas")]
        public async Task<IActionResult> GetFaturas(FaturaFiltro filtro)
       {
            var list = _mapper.Map<List<FaturaViewModel>>(_application.GetFaturasDaEmpresa(filtro));
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("Ciclos")]
        public async Task<IActionResult> GetCiclos()
        {
            var list = _mapper.Map<List<FaturaCicloViewModel>>(_applicationFaturaCiclo.GetAll());
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("ArquivoCobranca")]
        public async Task<IActionResult> GetArquivoCobranca()
        {
            var list = _mapper.Map<List<ArquivoCobrancaViewModel>>(_applicationArquivoCobranca.GetAll());
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("ArquivoCobranca")]
        public async Task<IActionResult> GetArquivoCobranca(IFormFile file)
        {
            var list = _mapper.Map<List<ArquivoCobrancaViewModel>>(_applicationArquivoCobranca.GetAll());
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("DownloadDemonstrativo")]
        public async Task<IActionResult> DownloadDemonstrativo(FaturaFiltro filtro)
        {
            var bytes = _application.GetFaturasDemonstrativo(filtro);
            string fileName = "Fatura_Demonstrativo.Xlsx";
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, fileName);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> GetEntregaDoccob([FromForm] UploadModel uploadModel)
        {
            var result = await _application.GetEntregaPorDoccob(uploadModel.files);

            result ??= new List<EntregaDemonstrativoDTO>();

            return await Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPost("EntregaPorPeriodo")]
        public async Task<IActionResult> GetEntregaPorPeriodo(FaturaPeriodoFiltro filtro)
        {
            var result = _application.GetEntregaPorPeriodo(filtro);
            return await Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPost("ProcessarFaturaManual")]
        public async Task<IActionResult> ProcessarFaturaManual(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            var bytes = await _application.ProcessarFaturaManual(entregaProcessamento);
            string fileName = "Fatura_Demonstrativo.Xlsx";
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, fileName);
        }

        [HttpPost("ProcessarFaturaAprovacao")]
        public async Task<IActionResult> ProcessarFaturaAprovacao(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            var faturaId = await _application.ProcessarFaturaAprovacao(entregaProcessamento);            
            return await Task.FromResult<IActionResult>(Ok(faturaId));
        }

        [HttpPost("Acao")]
        public async Task<IActionResult> ExecutarAcao(FaturaAcaoDto acao)
        {
            _application.RealizarAcao(acao);
            return await Task.FromResult<IActionResult>(Ok());
        }
    }
}
