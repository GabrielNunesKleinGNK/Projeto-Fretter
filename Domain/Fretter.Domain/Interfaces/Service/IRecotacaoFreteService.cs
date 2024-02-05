using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IRecotacaoFreteService<TContext> : IServiceBase<TContext, RecotacaoFrete>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessarRecotacaoFrete();
    }
}
