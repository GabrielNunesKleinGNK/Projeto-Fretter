using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using System.Data;
using Fretter.Domain.Dto.Fretter.Conciliacao;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IConciliacaoRepository<TContext> : IRepositoryBase<TContext, Conciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        void ProcessaConciliacao();
        void ProcessaFatura();
        List<EntregaConciliacaoDTO> ProcessaConciliacaoControle();
        void ProcessaConciliacaoControle(Int64 idControleProcesso);
        List<EntregaDemonstrativoDTO> GetDemonstrativoEntregaConciliacao(DataTable filtroDoccob, int empresaId,
            bool filtroPeriodo, DateTime? dataInicio = null, DateTime? dataTermino = null, int? transportadorId = null,
            int? statusConciliacaoId = null);
        List<EntregaConciliacaoRecotacaoDTO> ObterConciliacaoRecotacao();
        void ProcessaConciliacaoRecotacao(DataTable dataTable);
        ImportacaoArquivo GetArquivoPorConciliacao(int conciliacaoId);
        void EnviarParaRecalculoFrete(DataTable dataTable, string parametrosJson, int usuarioId);
        void EnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro, string parametrosJson, int usuarioId);
        List<ConciliacaoRecotacaoDTO> ListarRecotacoesPorIds(DataTable conciliacoes);
    }
}

