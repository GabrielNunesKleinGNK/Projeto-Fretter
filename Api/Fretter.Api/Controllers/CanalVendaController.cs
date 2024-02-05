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
    [Route("api/CanalVenda")]
    [ApiController]
    public class CanalVendaController : ControllerBase<FretterContext, CanalVenda, CanalVendaViewModel>
    {
        private readonly ICanalVendaApplication<FretterContext> _application;

        public CanalVendaController(ICanalVendaApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }
    }
}
