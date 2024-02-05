using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Controllers
{
    [Route("api/EntregaDevolucaoStatus")]
    [ApiController]
    public class EntregaDevolucaoStatusController : ControllerBase<FretterContext, EntregaDevolucaoStatus, EntregaDevolucaoStatusViewModel>
    {
        public new readonly IEntregaDevolucaoStatusApplication<FretterContext> _application;

        public EntregaDevolucaoStatusController(IEntregaDevolucaoStatusApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
    }
}
