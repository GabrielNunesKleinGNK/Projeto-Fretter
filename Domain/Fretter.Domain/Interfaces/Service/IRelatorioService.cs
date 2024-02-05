using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using Fretter.Domain.Dto.Relatorio;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IRelatorioService<TContext>
        where TContext : IUnitOfWork<TContext>
    {

        List<RelatorioEmpresa> GetRelatorioEmpresa(RelatorioFiltro filtro);
        List<RelatorioEmpresa> GetRelatorioEmpresaCategoria(RelatorioFiltro filtro);
        List<RelatorioLoja> GetRelatorioLoja(RelatorioFiltro filtro);
        List<RelatorioVendedor> GetRelatorioVendedor(RelatorioFiltro filtro);
        List<RelatorioProdutoMediaPreco> GetRelatorioProdutoMediaPreco(RelatorioFiltro filtro);
        RelatorioFiltro GetRelatorioFiltro();
        List<RelatorioCategoria> GetRelatorioCategoria(RelatorioFiltro filtro);
    }
}	
