using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Dto.EmpresaIntegracao;

namespace Fretter.Api.Controllers
{
    [Route("api/Integracao")]
    [ApiController]
    public class IntegracaoController : ControllerBase<FretterContext, Integracao, IntegracaoViewModel>
    {
        IIntegracaoApplication<FretterContext> _application;
        public IntegracaoController(IIntegracaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }

        [HttpGet("DePara")]
        public List<DeParaEmpresaIntegracao> BuscaCamposDePara()
        {
            return _application.BuscaCamposDePara();
        }

        [HttpPost("TesteIntegracao")]
        public Task<TesteIntegracaoRetorno> TesteIntegracao([FromBody] EmpresaIntegracaoViewModel dadosParaConsulta)
        {
            var result = _application.TesteIntegracao(dadosParaConsulta.Model());
            return result;
        }
    }
}
