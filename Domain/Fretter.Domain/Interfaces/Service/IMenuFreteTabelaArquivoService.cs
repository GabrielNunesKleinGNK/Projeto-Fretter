using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IMenuFreteTabelaArquivoService<TContext> : IServiceBase<TContext, TabelaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarTabelaArquivo();
    }
}
