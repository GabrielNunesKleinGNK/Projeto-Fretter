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
using Fretter.Domain.Dto.EntregaDevolucao;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaOcorrencia")]
    [ApiController]
    public class EntregaOcorrenciaController : ControllerBase<FretterContext, EntregaOcorrencia, EntregaOcorrenciaViewModel>
    {
        public readonly IEntregaOcorrenciaApplication<FretterContext> _application;
        public readonly IUsuarioHelper _user;

        public EntregaOcorrenciaController(IEntregaOcorrenciaApplication<FretterContext> application, IUsuarioHelper user) : base(application)
        {
            _user = user;
            _application = application;
        }

        [HttpPost("Inserir")]
        public async Task<IActionResult> Inserir([FromBody]List<EntregaOcorrenciaViewModel> ocorrencia)
        {
            var retorno = await _application.Inserir(_mapper.Map<List<EntregaOcorrencia>>(ocorrencia));
            return await Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadArquivoMassivo (IFormFile file)
        {
            var retorno = await _application.UploadArquivoMassivo(file);
            return await Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpPost("EntragasEmAberto")]
        public async Task<IActionResult> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro)
        {
            var retorno = await _application.ObterEntregasEmAberto(filtro);
            return await Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpGet("ObterDePara")]
        public async Task<IActionResult> ObterDePara()
        {
            var retorno = await _application.ObterDePara();
            return await Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpGet("ObterOcorrecia/{entregaId}")]
        public async Task<IActionResult> ObterOcorrencia(int entregaId)
        {
            var retorno = _application.GetAll(x => x.EntregaId == entregaId);
            return await Task.FromResult<IActionResult>(Ok(retorno));
        }

        [HttpPost("Download/{comEntregas}")]
        public async Task<IActionResult> DownloadArquivo(bool comEntregas, EntregaEmAbertoFiltro filtro)
        {
            try
            {
                var bytes = await _application.DownloadArquivo(comEntregas, filtro);
                return await Task.FromResult<IActionResult>(File(bytes, Helpers.Atributes.MimeTypes.Application.Xlsx, "Layout importacao de ocorrencias"));
            }
            catch (Exception ex)
            {
                if (ex.Message == "O filtro de 'data inicio x data fim' não deve ser maior que 31 dias.")
                {
                    return Conflict(ex.Message) ;
                }

                throw ex;
            }
         }
    }
}
