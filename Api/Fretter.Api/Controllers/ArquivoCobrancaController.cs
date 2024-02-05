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

namespace Fretter.Api.Controllers
{
    [Route("api/ArquivoCobranca")]
    [ApiController]
    public class ArquivoCobrancaController : ControllerBase<FretterContext, ArquivoCobranca, ArquivoCobrancaViewModel>
    {
        public new readonly IArquivoCobrancaApplication<FretterContext> _application;

        public ArquivoCobrancaController(IArquivoCobrancaApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("{faturaId}")]
        public override async Task<IActionResult> Get(int faturaId)
        {
            var list = _mapper.Map<List<ArquivoCobrancaViewModel>>(_application.ObterArquivosCobranca(faturaId));
            return await Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpPost("Upload/{faturaId}")]
        public async Task<IActionResult> UploadDoccob(IFormFile file, int faturaId)
        {
            await _application.SalvarUploadDoccob(file, faturaId);
            return await Task.FromResult<IActionResult>(Ok());
        }
    }
}
