using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IAtualizacaoTabelasCorreiosService<TContext> : IServiceBase<TContext, TabelasCorreiosArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        Task<TabelasCorreiosArquivo> UploadArquivo(IFormFile uploadModel);
        Task<TabelasCorreiosArquivo> ImportarArquivo(byte[] bytes, string fileName);
        Task ProcessarTabelasArquivo();
    }
}
