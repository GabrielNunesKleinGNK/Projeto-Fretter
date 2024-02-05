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

namespace Fretter.Api.Controllers
{
    [Route("api/EmpresaIntegracao")]
    [ApiController]
    public class EmpresaIntegracaoController : ControllerBase<FretterContext, EmpresaIntegracao, EmpresaIntegracaoViewModel>
    {
        IEmpresaIntegracaoApplication<FretterContext> _application;
        public EmpresaIntegracaoController(IEmpresaIntegracaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
