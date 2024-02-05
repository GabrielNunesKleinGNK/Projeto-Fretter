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

namespace Fretter.Api.Controllers
{
    [Route("api/AtualizacaoTabelaCorreios")]
    [ApiController]
    public class AtualizacaoTabelaCorreios : ControllerBase<FretterContext, TabelasCorreiosArquivo, TabelasCorreiosViewModel>
    {
        public new readonly IAtualizacaoTabelasCorreiosApplication<FretterContext> _application;
        public readonly IUsuarioHelper _user;

        public AtualizacaoTabelaCorreios(IAtualizacaoTabelasCorreiosApplication<FretterContext> application, IUsuarioHelper user) : base(application)
        {
            _user = user;
            _application = application;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadEmpresa(IFormFile file)
        {
            var arquivo = await _application.UploadArquivo(file);
            return await Task.FromResult<IActionResult>(Ok(arquivo));
        }
    }
}
