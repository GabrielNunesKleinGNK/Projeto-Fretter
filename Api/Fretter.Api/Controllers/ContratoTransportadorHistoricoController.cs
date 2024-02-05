
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;

namespace Fretter.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ContratoTransportadorHistoricoController : ControllerBase<FretterContext, ContratoTransportadorHistorico, ContratoTransportadorHistoricoViewModel>
    {
        IContratoTransportadorHistoricoApplication<FretterContext> _application;
        public ContratoTransportadorHistoricoController(IContratoTransportadorHistoricoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
