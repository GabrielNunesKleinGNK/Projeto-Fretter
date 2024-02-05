using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Dto.EntregaDevolucao;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaDevolucaoStatusAcao")]
    [ApiController]
    public class EntregaDevolucaoStatusAcaoController : ControllerBase<FretterContext, EntregaDevolucaoStatusAcao, EntregaDevolucaoStatusAcaoViewModel>
    {
        public new readonly IEntregaDevolucaoStatusAcaoApplication<FretterContext> _application;

        public EntregaDevolucaoStatusAcaoController(IEntregaDevolucaoStatusAcaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
