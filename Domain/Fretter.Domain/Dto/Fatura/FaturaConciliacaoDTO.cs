using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaConciliacaoDTO
    {
        public long ConciliacaoId { get; set; }
        public int TransportadorId { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string Observacao { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal? ValorAdicional { get; set; }
        public bool? Selecionado { get; set; }
    }
}
