using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Api.Models.Fusion;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Entities.Fusion.EDI;
using Fretter.Api.Models;

namespace Fretter.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/Produto")]
    [ApiController]
    public class ProdutoController : ControllerBase<FretterContext, Produto, ProdutoViewModel>
    {
        private readonly IProdutoApplication<FretterContext> _application;

        public ProdutoController(ICanalService<CommandContext> service, 
                               IProdutoApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }

        [HttpGet("ObterProdutoPorSku/{sku}")]
        public Task<IActionResult> ObterProdutoPorSku(string sku)
        {
            var t = _application.GetProdutoPorSku(sku);
            return Task.FromResult<IActionResult>(Ok(t));
        }

        [HttpGet("ObterProdutoPorDescricao/{descricao}")]
        public Task<IActionResult> ObterProdutoPorDescricao(string descricao)
        {
            var retorno = _application.GetProdutoPorDescricao(descricao);

            return Task.FromResult<IActionResult>(Ok(retorno));
        }
    }
}
