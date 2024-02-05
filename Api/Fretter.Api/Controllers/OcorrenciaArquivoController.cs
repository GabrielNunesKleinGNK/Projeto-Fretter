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
using Fretter.Domain.Interfaces;
using Fretter.Domain.Dto.EntregaDevolucao;
using System.Collections.Generic;

namespace Fretter.Api.Controllers
{
    [Route("api/OcorrenciaArquivo")]
    [ApiController]
    public class OcorrenciaArquivoController : ControllerBase<FretterContext, OcorrenciaArquivo, OcorrenciaArquivoViewModel>
    {
        public readonly IOcorrenciaArquivoApplication<FretterContext> _application;
        public readonly IUsuarioHelper _user;

        public OcorrenciaArquivoController(IOcorrenciaArquivoApplication<FretterContext> application, IUsuarioHelper user) : base(application)
        {
            _user = user;
            _application = application;
        }

    }
}
