using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class FaturaConciliacaoViewModel : IViewModel<FaturaConciliacao>
    {
        public int? FaturaId { get; set; }
        public long? ConciliacaoId { get; set; }
        public string Cnpj { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string Observacao { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal? ValorAdicional { get; set; }

        public FaturaConciliacao Model()
        {
            return new FaturaConciliacao(FaturaId, ConciliacaoId, Cnpj, NotaFiscal, Serie, DataEmissao, Observacao, ValorFrete, ValorAdicional);
        }
    }
}
