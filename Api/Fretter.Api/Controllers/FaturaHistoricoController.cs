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
    [Route("api/FaturaHistorico")]
    [ApiController]
    public class FaturaHistoricoController : ControllerBase<FretterContext, FaturaHistorico, FaturaHistoricoViewModel>
    {
        public new readonly IFaturaHistoricoApplication<FretterContext> _application;

        public FaturaHistoricoController(IFaturaHistoricoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("historico/{faturaId}")]
        public async Task<IActionResult> GetHistoricoDeFaturasPorEmpresa(int faturaId)
        {
            var list = _mapper.Map<List<FaturaHistoricoViewModel>>(_application.GetHistoricoDeFaturasPorEmpresa(faturaId));
            return await Task.FromResult<IActionResult>(Ok(list));
        }
    }
}
