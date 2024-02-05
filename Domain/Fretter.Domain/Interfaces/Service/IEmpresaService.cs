using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaService<TContext> : IServiceBase<TContext, Empresa>
        where TContext : IUnitOfWork<TContext>
    {
        Task ProcessarUploadEmpresa(IFormFile file);
        byte[] DownloadArquivo(int arquivoId);
    }
}	
