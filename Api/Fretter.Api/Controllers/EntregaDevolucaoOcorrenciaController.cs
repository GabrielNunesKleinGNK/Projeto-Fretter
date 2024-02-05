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
    [Route("api/EntregaDevolucaoOcorrencia")]
    [ApiController]
    public class EntregaDevolucaoOcorrenciaController : ControllerBase<FretterContext, EntregaDevolucaoOcorrencia, EntregaDevolucaoOcorrenciaViewModel>
    {
        public new readonly IEntregaDevolucaoOcorrenciaApplication<FretterContext> _application;

        public EntregaDevolucaoOcorrenciaController(IEntregaDevolucaoOcorrenciaApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("Ocorrencias/{entregaDevolucaoId}")]
        public async Task<IActionResult> GetOcorrenciasEntregaDevolucao(int entregaDevolucaoId)
        {
            var list = _mapper.Map<List<EntregaDevolucaoOcorrenciaViewModel>>(_application.GetAll(x => x.EntregaDevolucao == entregaDevolucaoId));
            return await Task.FromResult<IActionResult>(Ok(list));
        }
    }
}