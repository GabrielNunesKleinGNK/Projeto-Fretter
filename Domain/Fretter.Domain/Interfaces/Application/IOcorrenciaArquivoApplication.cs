using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IOcorrenciaArquivoApplication<TContext> : IApplicationBase<TContext, OcorrenciaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessarOcorrenciaArquivo();
    }
}
	