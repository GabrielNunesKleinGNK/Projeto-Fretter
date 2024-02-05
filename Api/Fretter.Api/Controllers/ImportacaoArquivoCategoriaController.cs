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
    [Route("api/ImportacaoArquivoCategoria")]
    [ApiController]
    public class ImportacaoArquivoCategoriaController : ControllerBase<FretterContext, ImportacaoArquivoCategoria, ImportacaoArquivoCategoriaViewModel>
    {
        
        public ImportacaoArquivoCategoriaController(IImportacaoArquivoCategoriaApplication<FretterContext> application) : base(application)
        {
        }
    }
}
