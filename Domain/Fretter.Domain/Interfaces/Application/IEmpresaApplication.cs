using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IEmpresaApplication<TContext> : IApplicationBase<TContext, Empresa>
        where TContext : IUnitOfWork<TContext>
    {
        Task ProcessarUploadEmpresa(IFormFile file);
        byte[] DownloadArquivo(int arquivoId);
    }
}
