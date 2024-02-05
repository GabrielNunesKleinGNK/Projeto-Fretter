using Fretter.Domain.Dto.CTe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fretter.Conciliacao
{
    public class RelatorioDetalhadoArquivoDTO
    {
        public long ConciliacaoId { get; set; }
        public string Transportador { get; set; }
        public string CodigoEntrega { get; set; }
        public string CodigoPedido { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal? EntregaPeso { get; set; }
        public decimal? EntregaAltura { get; set; }
        public decimal? EntregaComprimento { get; set; }
        public decimal? EntregaLargura { get; set; }
        public decimal? EntregaValorDeclarado { get; set; }
        public decimal? ValorDeclarado { get; set; }
        public decimal? ValorCustoFrete { get; set; }
        public decimal? ValorCustoReal { get; set; }
        public decimal? ValorICMS { get; set; }
        public decimal? ValorGRIS { get; set; }
        public decimal? ValorADValorem { get; set; }
        public decimal? ValorPedagio { get; set; }
        public decimal? ValorFretePeso { get; set; }
        public decimal? ValorTaxaTRT { get; set; }
        public decimal? ValorTaxaTDE { get; set; }
        public decimal? ValorTaxaTDA { get; set; }
        public decimal? ValorTaxaCTe { get; set; }
        public decimal? ValorTaxaRisco { get; set; }
        public decimal? ValorSuframa { get; set; }
        public string StatusConciliacao { get; set; }
    }
}
