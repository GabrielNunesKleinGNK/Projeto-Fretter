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
using System.Runtime.InteropServices;
using System.Linq;

namespace Fretter.Api.Controllers
{
    [Route("api/FaturaItem")]
    [ApiController]
    public class FaturaItemController : ControllerBase<FretterContext, FaturaItem, FaturaItemViewModel>
    {
        public new readonly IApplicationBase<FretterContext, FaturaItem> _application;

        public FaturaItemController(IApplicationBase<FretterContext, FaturaItem> application) : base(application)
        {
            _application = application;
        }

    }
}
