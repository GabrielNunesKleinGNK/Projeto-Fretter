using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaPeriodo : EntityBase
    {
        #region "Construtores"
        public FaturaPeriodo(int Id, int? FaturaCicloId, int? DiaVencimento, DateTime DataInicio, DateTime DataFim, bool Vigente, bool Processado,
            DateTime DataProcessamento, int? QuantidadeProcessado, int? DuracaoProcessamento)
        {
            this.Ativar();
            this.Id = Id;
            this.FaturaCicloId = FaturaCicloId;
            this.DiaVencimento = DiaVencimento;
            this.DataInicio = DataInicio;
            this.DataFim = DataFim;
            this.Vigente = Vigente;
            this.Processado = Processado;
            this.DataProcessamento = DataProcessamento;
            this.QuantidadeProcessado = QuantidadeProcessado;
            this.DuracaoProcessamento = DuracaoProcessamento;
        }
        #endregion

        #region "Propriedades"
        public int? FaturaCicloId { get; protected set; }
        public int? DiaVencimento { get; protected set; }
        public DateTime DataInicio { get; protected set; }
        public DateTime DataFim { get; protected set; }
        public bool Vigente { get; protected set; }
        public bool Processado { get; protected set; }
        public DateTime DataProcessamento { get; protected set; }
        public int? QuantidadeProcessado { get; protected set; }
        public int? DuracaoProcessamento { get; protected set; }


        #endregion

        #region "Referencias"
        public virtual List<Fatura> Fatura { get; set; }
        #endregion

        #region "Métodos"
        #endregion
    }
}
