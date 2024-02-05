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
using Fretter.Domain.Interfaces;
using Fretter.Domain.Dto.ImportacaoArquivo;

namespace Fretter.Api.Controllers
{
    [Route("api/ImportacaoArquivo")]
    [ApiController]
    public class ImportacaoArquivoController : ControllerBase<FretterContext, ImportacaoArquivo, ImportacaoArquivoViewModel>
    {
        public new readonly IImportacaoArquivoApplication<FretterContext> _application;
        public readonly IUsuarioHelper _user;

        public ImportacaoArquivoController(IImportacaoArquivoApplication<FretterContext> application, IUsuarioHelper user) : base(application)
        {
            _user = user;
            _application = application;
        }

        [HttpPost("resumo")]
        public Task<IActionResult> ObterImportacaoArquivoResumo([FromBody] ImportacaoArquivoFiltro importacaoArquivoFiltro)
        {
            return Task.FromResult<IActionResult>(Ok(_application.ObterImportacaoArquivoResumo(importacaoArquivoFiltro)));
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadArquivo([FromForm] UploadModel uploadModel)
        {
            await _application.UploadArquivo(uploadModel, _user.UsuarioLogado.EmpresaId);
            return await Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost("Download")]
        public string DownloadArquivo(UploadModel uploadModel)
        {
            return _application.DownloadArquivo(uploadModel);
        }
    }
}
