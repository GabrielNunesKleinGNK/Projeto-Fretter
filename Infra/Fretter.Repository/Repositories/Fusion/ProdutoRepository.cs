using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Linq;
using Fretter.Domain.Entities.Fusion.EDI;
using System.Collections.Generic;

namespace Fretter.Repository.Repositories
{
    public class ProdutoRepository<TContext> : RepositoryBase<TContext, Produto>, IProdutoRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Produto> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Produto>();
        
        public ProdutoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public Produto GetProdutoPorSku(string sku, int empresaId)
        {
            return _dbSet.FirstOrDefault(x => x.EmpresaId == empresaId && x.Codigo == sku );
        }

        public IEnumerable<Produto> GetProdutoPorDescricao(string descricao, int empresaId)
        {
            return _dbSet.Where(x => x.EmpresaId == empresaId && x.Nome.Contains(descricao)).Take(100);
        }
    }
}	
