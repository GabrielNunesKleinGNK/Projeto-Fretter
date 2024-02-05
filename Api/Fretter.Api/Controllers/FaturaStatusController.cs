using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;

namespace Fretter.Api.Controllers
{
    ///[Authorize("Bearer")]
    [Route("api/faturastatus")]
    [ApiController]
    public class FaturaStatusController : ControllerBase<FretterContext, FaturaStatus, FaturaStatusViewModel>
    {
        private readonly IApplicationBase<FretterContext, FaturaStatus> _application;

        public FaturaStatusController(IApplicationBase<FretterContext, FaturaStatus> application) : base(application)
        {
            _application = application;
        }
    }
}
