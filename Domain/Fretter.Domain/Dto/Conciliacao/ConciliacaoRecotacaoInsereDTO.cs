using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class ConciliacaoRecotacaoInsereDTO
    {
        public long ConciliacaoRecotacaoId { get; set; }
        public long ConciliacaoId { get; set; }
        public string Protocolo { get; set; }
        public string JsonValoresRecotacao { get; set; }
        public string JsonRetornoRecotacao { get; set; }
        public bool Sucesso { get; set; }
        public decimal? ValorCustoFrete { get; set; }
        public decimal? ValorCustoAdicional { get; set; }
        public int? TabelaId { get; set; }
    }
}
