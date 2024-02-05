using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Api.Models.Fusion;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/empresa")]
    [ApiController]
    public class EmpresaController : ControllerBase<FretterContext, Empresa, EmpresaViewModel>
    {
        public new readonly IEmpresaApplication<FretterContext> _application;
        public readonly IEmpresaImportacaoApplication<FretterContext> _empresaImportacaoApplication;
        public readonly IApplicationBase<FretterContext, EmpresaImportacaoDetalhe> _empresaImportacaoDetalheApplication;
        public readonly IUsuarioHelper _user;
        private readonly FretterConfig _fretterConfig;

        public EmpresaController(IEmpresaApplication<FretterContext> application,
            IEmpresaImportacaoApplication<FretterContext> empresaImportacaoApplication,
            IApplicationBase<FretterContext, EmpresaImportacaoDetalhe> empresaImportacaoDetalheApplication,
            IUsuarioHelper user,
            IOptions<FretterConfig> fretterConfig) : base(application)
        {
            _application = application;
            _empresaImportacaoApplication = empresaImportacaoApplication;
            _empresaImportacaoDetalheApplication = empresaImportacaoDetalheApplication;
            _user = user;
            _fretterConfig = fretterConfig.Value;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadEmpresa(IFormFile file)
        {
            if (_user.UsuarioLogado.EmpresaId != _fretterConfig.EmpresaImportacaoReferenciaId)
                return await Task.FromResult<IActionResult>(BadRequest("Empresa não autorizada a realizar essa ação !"));

            await _application.ProcessarUploadEmpresa(file);
            return await Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost("Importacao")]
        public Task<IActionResult> GetEmpresaImportacao(EmpresaImportacaoFiltro filtro)
        {
            var list = _mapper.Map<List<EmpresaImportacaoArquivoViewModel>>(_empresaImportacaoApplication.GetEmpresaImportacaoArquivoPorEmpresa(filtro));
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("Importacao/Detalhe/{arquivoId}")]
        public Task<IActionResult> GetEmpresaImportacaoDetalhe(int arquivoId)
        {
            var list = _mapper.Map<List<EmpresaImportacaoDetalheViewModel>>(_empresaImportacaoDetalheApplication.GetAll(x => x.EmpresaImportacaoArquivoId == arquivoId));
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("Download")]
        public async Task<IActionResult> DownloadArquivo(UploadModel model)
        {
            var bytes = _application.DownloadArquivo(model.id);
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, model.fileName);
        }

        [HttpGet("DownloadTemplate")]
        public async Task<IActionResult> DownloadTemplate()
        {
            string localFilePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"./Template\Importacao_Empresa.xlsx" : @"./Template/Importacao_Empresa.xlsx";
            string fileName = "Importacao_Empresa.xlsx";

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var bytes = await System.IO.File.ReadAllBytesAsync(localFilePath);
            return File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, fileName);
        }

        [HttpGet("ObterEmpresaLogada")]
        public Task<IActionResult> ObterEmpresaLogada()
        {
            var list = _mapper.Map<List<EmpresaViewModel>>(_application.GetAll(x => x.Id == _user.UsuarioLogado.EmpresaId));
            return Task.FromResult<IActionResult>(Ok(list));
        }
    }
}
