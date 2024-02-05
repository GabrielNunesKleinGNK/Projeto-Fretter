using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaConciliacao : EntityBase
    {
        #region "Construtores"
        protected FaturaConciliacao() { }

        public FaturaConciliacao(int? faturaId, long? conciliacaoId, string cnpj, string notaFiscal, string serie, DateTime? dataEmissao, string observacao, decimal? valorFrete, decimal? valorAdicional)
        {
            FaturaId = faturaId;
            ConciliacaoId = conciliacaoId;
            Cnpj = cnpj;
            NotaFiscal = notaFiscal;
            Serie = serie;
            DataEmissao = dataEmissao;
            Observacao = observacao;
            ValorFrete = valorFrete;
            ValorAdicional = valorAdicional;
        }


        #endregion

        #region "Propriedades"
        public int? FaturaId { get; protected set; }
        public long? ConciliacaoId { get; protected set; }
        public string Cnpj { get; protected set; }
        public string NotaFiscal { get; protected set; }
        public string Serie { get; protected set; }
        public DateTime? DataEmissao { get; protected set; }
        public string Observacao { get; protected set; }
        public decimal? ValorFrete { get; protected set; }
        public decimal? ValorAdicional { get; protected set; }        
        #endregion

        #region "Referencias"
        public virtual Fatura Fatura { get; set; }
        public virtual Conciliacao Conciliacao { get; set; }        
        #endregion

        #region "Métodos"      
        #endregion
    }
}
