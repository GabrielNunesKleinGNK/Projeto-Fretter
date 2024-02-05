using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;
using Fretter.Domain.Enum;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class ProdutoService<TContext> : ServiceBase<TContext, Produto>, IProdutoService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IProdutoRepository<TContext> _Repository;

        public ProdutoService
        (
            IProdutoRepository<TContext> Repository,
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user
        ) 
            : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;         
        }

        public Produto GetProdutoPorSku(string sku)
        {
            return _Repository.GetProdutoPorSku(sku, _user.UsuarioLogado.EmpresaId);
        }
        public IEnumerable<Produto> GetProdutoPorDescricao(string descricao)
        {
            return _Repository.GetProdutoPorDescricao(descricao, _user.UsuarioLogado.EmpresaId);
        }
    }
}
