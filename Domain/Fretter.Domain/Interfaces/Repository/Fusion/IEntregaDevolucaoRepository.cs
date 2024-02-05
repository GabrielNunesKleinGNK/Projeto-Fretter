using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.LogisticaReversa;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaDevolucaoRepository<TContext> : IRepositoryBase<TContext, EntregaDevolucao>
        where TContext : IUnitOfWork<TContext>
    {
        List<DevolucaoCorreio> BuscaEntregaDevolucaoPendente();
        List<EntregaDevolucao> GetEntregasDevolucoes(Expression<Func<EntregaDevolucao, bool>> predicate = null);
        void SalvarEntregaDevolucaoProcessada(DataTable entregaDevolucao);
        void SalvarEntregaDevolucaoOcorrencia(DataTable entregaDevolucaoOcorrencia);
        List<DevolucaoCorreioCancela> BuscaEntregaDevolucaoCancela();
        List<DevolucaoCorreioRetorno> ProcessarEntregaDevolucaoOcorrencia(DataTable entregaDevolucaoOcorrencia);
        void InserirEntrega();
    }
}

