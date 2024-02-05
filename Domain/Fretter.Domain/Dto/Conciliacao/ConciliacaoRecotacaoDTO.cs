using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class ConciliacaoRecotacaoDTO
    {
        public long ConciliacaoRecotacaoId { get; set; }
        public long ConciliacaoId { get; set; }
        public decimal? ValorCustoFrete { get; set; }
        public decimal? ValorCustoAdicional { get; set; }
        public decimal? ValorCustoReal { get; set; }
        public string JsonRetornoRecotacao { get; set; }
        public int TabelaId { get; set; }
        public string TabelaDescricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataProcessamento { get; set; }
    }
}
