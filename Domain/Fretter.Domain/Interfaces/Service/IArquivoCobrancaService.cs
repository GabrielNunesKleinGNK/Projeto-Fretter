using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IArquivoCobrancaService<TContext> : IServiceBase<TContext, ArquivoCobranca>
        where TContext : IUnitOfWork<TContext>
    {
        List<ArquivoCobranca> ObterArquivosCobranca(int faturaId);
        Task SalvarUploadDoccob(IFormFile file, int faturaId);
        Task<string> RealizarUploadDoccob(IFormFile file);
    }
}	
