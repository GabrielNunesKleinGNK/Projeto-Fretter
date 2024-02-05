using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Dto.EntregaDevolucao;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaDevolucaoHistorico")]
    [ApiController]
    public class EntregaDevolucaoHistoricoController : ControllerBase<FretterContext, EntregaDevolucaoHistorico, EntregaDevolucaoHistoricoViewModel>
    {
        public new readonly IEntregaDevolucaoHistoricoApplication<FretterContext> _application;

        public EntregaDevolucaoHistoricoController(IEntregaDevolucaoHistoricoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("obter/{entregaDevolucaoId}")]
        public async Task<IActionResult> GetHistoricoEntregaDevolucao(int entregaDevolucaoId)
        {
            var list = _mapper.Map<List<EntregaDevolucaoHistoricoViewModel>>(_application.ObterHistoricoEntregaDevolucao(entregaDevolucaoId));
            return await Task.FromResult<IActionResult>(Ok(list));
        }
    }
}