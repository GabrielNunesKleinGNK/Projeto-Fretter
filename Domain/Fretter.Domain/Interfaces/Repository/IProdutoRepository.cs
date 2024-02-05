using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Entities.Fusion.EDI;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IProdutoRepository<TContext> : IRepositoryBase<TContext, Produto>
        where TContext : IUnitOfWork<TContext>
    {
        Produto GetProdutoPorSku(string sku, int empresaId);

        IEnumerable<Produto> GetProdutoPorDescricao(string descricao, int empresaId);
    }
}
