using Fretter.Api.Models;
using Fretter.Domain.Dto.ImportacaoArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IImportacaoArquivoApplication<TContext> : IApplicationBase<TContext, ImportacaoArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro);
        Task UploadArquivo(UploadModel uploadModel, int empresaId);
        string DownloadArquivo(UploadModel uploadModel);
    }
}
	