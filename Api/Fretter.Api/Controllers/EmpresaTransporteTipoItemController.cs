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

namespace Fretter.Api.Controllers
{
    [Route("api/EmpresaTransporteTipoItem")]
    [ApiController]
    public class EmpresaTransporteTipoItemController : ControllerBase<FretterContext, EmpresaTransporteTipoItem, EmpresaTransporteTipoItemViewModel>
    {
        private readonly IEmpresaTransporteTipoItemApplication<FretterContext> _application;

        public EmpresaTransporteTipoItemController(IEmpresaTransporteTipoItemApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }

        [HttpGet("obterPorTipo/{transporteTipoId}")]
        public Task<IActionResult> GetTransporteItemPorTipo(int transporteTipoId)
        {
            var list = _application.GetEmpresaTransporteItemPorTipo(transporteTipoId);
            return Task.FromResult<IActionResult>(Ok(_mapper.Map<IList<EmpresaTransporteTipoItemViewModel>>(list)));
        }
    }
}
