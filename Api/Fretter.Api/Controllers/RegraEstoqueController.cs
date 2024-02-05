
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Domain.Interfaces.Application;
using Fretter.Repository.Contexts;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Api.Models.Fusion;

namespace Fretter.Api.Controllers
{
    [Route("api/RegraEstoque")]
    [ApiController]
    public class RegraEstoqueController : ControllerBase<FretterContext, RegraEstoque, RegraEstoqueViewModel>
    {
        public RegraEstoqueController(IRegraEstoqueApplication<FretterContext> application) : base(application)
        {

        }
    }
}
