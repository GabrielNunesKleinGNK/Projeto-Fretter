using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Dto.EmpresaIntegracao;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaOrigemImportacao")]
    [ApiController]
    public class EntregaOrigemImportacaoController : ControllerBase<FretterContext, EntregaOrigemImportacao, EntregaOrigemImportacaoViewModel>
    {
        IEntregaOrigemImportacaoApplication<FretterContext> _application;
        public EntregaOrigemImportacaoController(IEntregaOrigemImportacaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
