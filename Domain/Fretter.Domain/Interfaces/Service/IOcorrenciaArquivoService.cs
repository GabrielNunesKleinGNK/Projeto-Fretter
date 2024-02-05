using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IOcorrenciaArquivoService<TContext> : IServiceBase<TContext, OcorrenciaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarOcorrenciaArquivo();
    }
}
