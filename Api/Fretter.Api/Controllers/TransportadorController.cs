using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Entities.Fusion;
using Fretter.Api.Models.Fusion;
using Fretter.IoC;

namespace Fretter.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class TransportadorController : ControllerBase//: ControllerBase<FretterContext, Empresa, EmpresaViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly ITransportadorService<FretterContext> _service;
        public readonly IMapper _mapper = ServiceLocator.Resolve<IMapper>();

        public TransportadorController(ITransportadorService<FretterContext> service, 
                                      IUsuarioHelper user)
        {
            _user = user;
            _service = service;
        }

        [HttpGet]
        public Task<IActionResult> GetTransportadores()
        {
            var list = _service.GetTransportador();
            return Task.FromResult<IActionResult>(Ok(_mapper.Map<IList<TransportadorViewModel>>(list)));
        }

        [HttpGet("transportadoresPorEmpresa")]
        public Task<IActionResult> GetTransportadorPorEmpresa()
        {
            var list = _service.GetTransportadorPorEmpresa();
            return Task.FromResult<IActionResult>(Ok(list));
        }

        [HttpGet("Cnpj/{transportadorId}")]
        public Task<IActionResult> GetTransportadoresCnpj(int transportadorId)
        {
            var list = _service.GetTransportadorCNPJ(transportadorId);
            return Task.FromResult<IActionResult>(Ok(_mapper.Map<IList<TransportadorCnpjViewModel>>(list)));
        }
    }
}
