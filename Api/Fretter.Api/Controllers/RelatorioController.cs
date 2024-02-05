using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Dto.Relatorio;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;

namespace Fretter.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/Relatorio")]
    [ApiController]
    public class RelatorioController :  ControllerBase//: ControllerBase<FretterContext, Empresa, EmpresaViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly IRelatorioService<CommandContext> _service;

        public RelatorioController(IRelatorioService<CommandContext> service, IUsuarioHelper user)
        {
            _user = user;
            _service = service;
        }

        [HttpPost("loja")]
        public Task<IActionResult> GetRelatorioLoja(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioLoja(filtro)));
        }

        [HttpPost("produtomediapreco")]
        public Task<IActionResult> GetRelatorioProdutoMediaPreco(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioProdutoMediaPreco(filtro)));
        }

        [HttpPost("empresa")]
        public Task<IActionResult> GetRelatorioEmpresa(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioEmpresa(filtro)));
        }

        [HttpPost("empresa/categoria")]
        public Task<IActionResult> GetRelatorioEmpresaCategoria(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioEmpresaCategoria(filtro)));
        }

        [HttpPost("vendedor")]
        public Task<IActionResult> GetRelatorioVendedor(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioVendedor(filtro)));
        }

        [HttpPost("categoria")]
        public Task<IActionResult> GetRelatorioCategoria(RelatorioFiltro filtro)
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetRelatorioCategoria(filtro)));
        }
    }
}
