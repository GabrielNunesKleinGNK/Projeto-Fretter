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
using System;
using Fretter.Domain.Dto.EmpresaTransporteTipoItem;

namespace Fretter.Api.Controllers
{
    [Route("api/EmpresaTransporteConfiguracaoItem")]
    [ApiController]
    public class EmpresaTransporteConfiguracaoItemController : ControllerBase<FretterContext, EmpresaTransporteConfiguracaoItem, EmpresaTransporteConfiguracaoItemViewModel>
    {
        private readonly IEmpresaTransporteConfiguracaoItemApplication<FretterContext> _application;

        public EmpresaTransporteConfiguracaoItemController(IEmpresaTransporteConfiguracaoItemApplication<FretterContext> application) 
            : base(application)
        {
            _application = application;
        }
    }
}
