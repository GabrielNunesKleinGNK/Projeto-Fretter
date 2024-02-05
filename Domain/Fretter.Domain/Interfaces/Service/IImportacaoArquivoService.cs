using Fretter.Api.Models;
using Fretter.Domain.Dto.ImportacaoArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoArquivoService<TContext> : IServiceBase<TContext, ImportacaoArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro);
        Task UploadArquivo(UploadModel uploadModel, int empresaId);
        string DownloadArquivo(UploadModel uploadModel);
        Task ImportarArquivo(byte[] bytes, string fileName, int empresaId, int configuracaoId);
        void ProcessarArquivosCTe();
        void ProcessarArquivos();
    }
}
