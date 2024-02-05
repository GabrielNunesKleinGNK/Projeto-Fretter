using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Entities.Fusion;
using Fretter.Api.Models.Fusion;
using Fretter.IoC;
using Fretter.Api.Helpers;

namespace Fretter.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class TransportadorCnpjController : ControllerBase<FretterContext, TransportadorCnpj, TransportadorCnpjViewModel>
    {
        private readonly IUsuarioHelper _user;
        private readonly IApplicationBase<FretterContext, TransportadorCnpj> _application;
        public readonly IMapper _mapper = ServiceLocator.Resolve<IMapper>();

        public TransportadorCnpjController(IApplicationBase<FretterContext, TransportadorCnpj> application,
                                      IUsuarioHelper user) : base(application)
        {
            _application = application;
        } 

    }
}
