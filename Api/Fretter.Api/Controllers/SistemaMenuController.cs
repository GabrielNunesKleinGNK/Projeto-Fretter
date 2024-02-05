using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Api.Helpers;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/SistemaMenu")]
    [ApiController]
    public class SistemaMenuController : ControllerBase<FretterContext, SisMenu, SisMenuViewModel>
    {
        private readonly ISisMenuApplication<FretterContext> _application;

        public SistemaMenuController(ISisMenuApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }


        [HttpGet("Menus")]
        public Task<IActionResult> GetMenus()
        {
            return Task.FromResult<IActionResult>(Ok(_application.GetUserMenu()));
        }
        
    }
}
