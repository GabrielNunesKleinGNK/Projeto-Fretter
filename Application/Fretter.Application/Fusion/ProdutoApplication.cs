using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ProdutoApplication<TContext> : ApplicationBase<TContext, Produto>, IProdutoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IProdutoService<TContext> _service;

        public ProdutoApplication(IUnitOfWork<TContext> context, IProdutoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public Produto GetProdutoPorSku(string sku)
        {
            return _service.GetProdutoPorSku(sku);
        }
        public IEnumerable<Produto> GetProdutoPorDescricao(string descricao)
        {
            return _service.GetProdutoPorDescricao(descricao);
        }
    }
}	
