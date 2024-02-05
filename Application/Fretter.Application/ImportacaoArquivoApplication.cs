
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Dto.ImportacaoArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Fretter.Application
{
    public class ImportacaoArquivoApplication<TContext> : ApplicationBase<TContext, ImportacaoArquivo>, IImportacaoArquivoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IImportacaoArquivoService<TContext> _service;
        public ImportacaoArquivoApplication(IUnitOfWork<TContext> context, IImportacaoArquivoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro)
        {
            return _service.ObterImportacaoArquivoResumo(importacaoArquivoFiltro);
        }

        public string DownloadArquivo(UploadModel uploadModel)
        {
            return _service.DownloadArquivo(uploadModel);
        }

        public async Task UploadArquivo(UploadModel uploadModel, int empresaId)
        {
            await _service.UploadArquivo(uploadModel, empresaId);
            _unitOfWork.Commit();
        }
    }
}	
