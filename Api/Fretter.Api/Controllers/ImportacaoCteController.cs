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

namespace Fretter.Api.Controllers
{
    [Route("api/ImportacaoCte")]
    [ApiController]
    public class ImportacaoCteController : ControllerBase<FretterContext, ImportacaoCte, ImportacaoCteViewModel>
    {
        
        public ImportacaoCteController(IImportacaoCteApplication<FretterContext> application) : base(application)
        {
        }
    }
}
