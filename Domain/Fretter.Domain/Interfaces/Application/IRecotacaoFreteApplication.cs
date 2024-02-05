using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IRecotacaoFreteApplication<TContext> : IApplicationBase<TContext, RecotacaoFrete>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessarArquivoRecotacaoAsync();
    }
}
	