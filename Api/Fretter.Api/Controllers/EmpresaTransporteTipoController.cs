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
    [Route("api/EmpresaTransporteTipo")]
    [ApiController]
    public class EmpresaTransporteTipoController : ControllerBase<FretterContext, EmpresaTransporteTipo, EmpresaTransporteTipoViewModel>
    {
        private readonly IEmpresaTransporteTipoApplication<FretterContext> _application;

        public EmpresaTransporteTipoController(IEmpresaTransporteTipoApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }
    }
}
