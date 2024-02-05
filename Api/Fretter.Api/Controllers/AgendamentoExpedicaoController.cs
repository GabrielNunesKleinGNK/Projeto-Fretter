using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoExpedicaoController : ControllerBase<FretterContext, AgendamentoExpedicao, AgendamentoExpedicaoViewModel>
    {
        IAgendamentoExpedicaoApplication<FretterContext> _application;
        public AgendamentoExpedicaoController(IAgendamentoExpedicaoApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
