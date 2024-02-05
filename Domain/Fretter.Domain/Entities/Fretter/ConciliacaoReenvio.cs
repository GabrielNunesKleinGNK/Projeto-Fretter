using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class ConciliacaoReenvio : EntityBase
    {
        #region "Construtores"
        public ConciliacaoReenvio(long faturaConciliacaoId, int faturaId, long conciliacaoId)
        {
            AtualizarFaturaConciliacaoId(faturaConciliacaoId);
            AtualizarFaturaId(faturaId);
            AtualizarConciliacaoId(conciliacaoId);
        }
        #endregion

        #region "Propriedades"
        public long FaturaConciliacaoId { get; protected set; }
        public int FaturaId { get; protected set; }
        public long ConciliacaoId { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarFaturaConciliacaoId(long faturaConciliacaoId) => this.FaturaConciliacaoId = faturaConciliacaoId;
        public void AtualizarFaturaId(int faturaId) => this.FaturaId = faturaId;
        public void AtualizarConciliacaoId(long conciliacaoId) => this.ConciliacaoId = conciliacaoId;
        #endregion
    }
}