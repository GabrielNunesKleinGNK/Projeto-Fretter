using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Entities.Fusion.EDI;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IProdutoApplication<TContext> : IApplicationBase<TContext, Produto>
        where TContext : IUnitOfWork<TContext>
    {
        Produto GetProdutoPorSku(string sku);
        IEnumerable<Produto> GetProdutoPorDescricao(string descricao);
    }
}
