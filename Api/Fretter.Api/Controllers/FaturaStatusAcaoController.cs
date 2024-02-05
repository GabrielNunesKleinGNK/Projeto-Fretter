using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Dto.Fatura;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/FaturaStatusAcao")]
    [ApiController]
    public class FaturaStatusAcaoController : ControllerBase<FretterContext, FaturaStatusAcao, FaturaStatusAcaoViewModel>
    {
        public new readonly IFaturaStatusAcaoApplication<FretterContext> _application;

        public FaturaStatusAcaoController(IFaturaStatusAcaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
