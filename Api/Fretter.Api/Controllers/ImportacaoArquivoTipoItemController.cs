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
    [Route("api/ImportacaoArquivoTipoItem")]
    [ApiController]
    public class ImportacaoArquivoTipoItemController : ControllerBase<FretterContext, ImportacaoArquivoTipoItem, ImportacaoArquivoTipoItemViewModel>
    {
        private readonly IImportacaoArquivoTipoItemApplication<FretterContext> _application;
         
        public ImportacaoArquivoTipoItemController(IImportacaoArquivoTipoItemApplication<FretterContext> application) : base(application)
        {
            _application = application;
        }
        [HttpGet("getTipoCobrancaPorTipoArquivo/{importacaoArquivoTipoId}")]
        public List<ImportacaoArquivoTipoItemViewModel> getTipoCobrancaPorTipoArquivo(int importacaoArquivoTipoId)
        {
            var result = _mapper.Map<List<ImportacaoArquivoTipoItemViewModel>>(_application.GetAll(x => x.ImportacaoArquivoTipoId == importacaoArquivoTipoId));
            return result;
        }
    }
}
