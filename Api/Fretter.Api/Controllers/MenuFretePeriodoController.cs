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
    [Route("api/MenuPeriodo")]
    [ApiController]
    public class MenuFretePeriodoController : ControllerBase<FretterContext, MenuFretePeriodo, MenuFretePeriodoViewModel>
    {
        private readonly IApplicationBase<FretterContext, MenuFretePeriodo> _application;

        public MenuFretePeriodoController(IApplicationBase<FretterContext, MenuFretePeriodo> application) 
            : base(application)
        {
            _application = application;
        }
    }
}
