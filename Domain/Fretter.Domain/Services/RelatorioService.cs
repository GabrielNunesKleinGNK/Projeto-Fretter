using System.Collections.Generic;
using Fretter.Domain.Dto.Relatorio;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class RelatorioService<TContext> : IRelatorioService<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        private readonly IGenericRepository<TContext> _repository;
        private readonly IUsuarioHelper _user;

        public RelatorioService(IGenericRepository<TContext> repository, IUsuarioHelper user) 
        {
            _repository = repository;
            _user = user;
        }

        public List<RelatorioEmpresa> GetRelatorioEmpresa(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;
            return _repository.ExecuteStoredProcedure<RelatorioEmpresa, RelatorioFiltro>(filtro, "GetRelatorioEmpresa");
        }

        public List<RelatorioEmpresa> GetRelatorioEmpresaCategoria(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;
            return _repository.ExecuteStoredProcedure<RelatorioEmpresa, RelatorioFiltro>(filtro, "GetRelatorioEmpresaCategoria");
        }

        public RelatorioFiltro GetRelatorioFiltro()
        {
            return new RelatorioFiltro();
        }

        public List<RelatorioLoja> GetRelatorioLoja(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;

            return _repository.ExecuteStoredProcedure<RelatorioLoja, RelatorioFiltro>(filtro, "GetRelatorioLoja");
        }

        public List<RelatorioProdutoMediaPreco> GetRelatorioProdutoMediaPreco(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;

            return _repository.ExecuteStoredProcedure<RelatorioProdutoMediaPreco, RelatorioFiltro>(filtro, "GetRelatorioProdutoMediaPreco");
        }

        public List<RelatorioVendedor> GetRelatorioVendedor(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;

            return _repository.ExecuteStoredProcedure<RelatorioVendedor, RelatorioFiltro>(filtro, "GetRelatorioVendedor");
        }

        public List<RelatorioCategoria> GetRelatorioCategoria(RelatorioFiltro filtro)
        {
            filtro.UsuarioLogado = _user.UsuarioLogado.Id;

            return _repository.ExecuteStoredProcedure<RelatorioCategoria, RelatorioFiltro>(filtro, "GetRelatorioCategoria");
        }
    }
}	
