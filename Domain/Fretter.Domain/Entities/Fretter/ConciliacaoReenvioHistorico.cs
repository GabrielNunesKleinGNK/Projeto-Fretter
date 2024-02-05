using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class ConciliacaoReenvioHistorico : EntityBase
    {
        #region "Construtores"
        public ConciliacaoReenvioHistorico(int conciliacaoReenvioId, long faturaConciliacaoId, int faturaId, long conciliacaoId, DateTime dataReprocessamento)
        {
            AtualizarConciliacaoReenvioId(conciliacaoReenvioId);
            AtualizarFaturaConciliacaoId(faturaConciliacaoId);
            AtualizarFaturaId(faturaId);
            AtualizarConciliacaoId(conciliacaoId);
            AtualizarDataReprocessamento(dataReprocessamento);
        }
        #endregion

        #region "Propriedades"
        public int ConciliacaoReenvioId { get; protected set; }
        public long FaturaConciliacaoId { get; protected set; }
        public int FaturaId { get; protected set; }
        public long ConciliacaoId { get; protected set; }
        public DateTime DataReprocessamento { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarConciliacaoReenvioId(int conciliacaoReenvioId) => this.ConciliacaoReenvioId = conciliacaoReenvioId;
        public void AtualizarFaturaConciliacaoId(long faturaConciliacaoId) => this.FaturaConciliacaoId = faturaConciliacaoId;
        public void AtualizarFaturaId(int faturaId) => this.FaturaId = faturaId;
        public void AtualizarConciliacaoId(long conciliacaoId) => this.ConciliacaoId = conciliacaoId;
        public void AtualizarDataReprocessamento(DateTime dataReprocessamento) => this.DataReprocessamento = dataReprocessamento;
        #endregion
    }
}