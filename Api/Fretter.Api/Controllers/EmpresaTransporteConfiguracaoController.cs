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
using System;
using Fretter.Domain.Dto.EmpresaTransporteTipoItem;

namespace Fretter.Api.Controllers
{
    [Route("api/EmpresaTransporteConfiguracao")]
    [ApiController]
    public class EmpresaTransporteConfiguracaoController : ControllerBase<FretterContext, EmpresaTransporteConfiguracao, EmpresaTransporteConfiguracaoViewModel>
    {
        private readonly IEmpresaTransporteConfiguracaoApplication<FretterContext> _application;

        public EmpresaTransporteConfiguracaoController(IEmpresaTransporteConfiguracaoApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }

        [HttpPost("TesteIntegracao")]
        public Task<EmpresaTransporteConfiguracao> TesteIntegracao([FromBody]EmpresaTransporteConfiguracaoViewModel dadosParaConsulta)
        {
            var result = _application.TesteIntegracao(dadosParaConsulta.Model());
            return result;
        }
    }
}
