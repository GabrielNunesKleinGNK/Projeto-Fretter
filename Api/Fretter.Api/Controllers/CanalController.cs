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
    [Authorize("Bearer")]
    [Route("api/Canal")]
    [ApiController]
    public class CanalController : ControllerBase<FretterContext, Canal, CanalViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly ICanalService<CommandContext> _service;

        public CanalController(ICanalService<CommandContext> service, 
                               ICanalApplication<FretterContext> application) 
            : base(application)
        {
            _service = service;
        }

        [HttpGet("CanaisPorEmpresa")]
        public Task<IActionResult> GetCanalPorEmpresa()
        {
            var t = _service.GetCanalPorEmpresa();
            return Task.FromResult<IActionResult>(Ok(t));
        }
    }
}
