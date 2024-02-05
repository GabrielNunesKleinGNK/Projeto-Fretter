using DocumentFormat.OpenXml.Math;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Helpers.Attributes;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.Fatura
{
    public class EntregaDemonstrativoDTO
    {
        public long ConciliacaoId { get; set; }
        [DataTableColumnIgnore]
        public decimal ValorCustoFrete { get; set; }
        [DataTableColumnIgnore]
        public decimal ValorCustoReal { get; set; }
        [DataTableColumnIgnore]
        public decimal ValorFreteDoccob { get; set; }
        [DataTableColumnIgnore]
        public int TransportadorId { get; set; }
        [DataTableColumnIgnore]
        public string Transportador { get; set; }
        [DataTableColumnIgnore]
        public string ConciliacaoTipo { get; set; }
        [DataTableColumnIgnore]
        public string CodigoPedido { get; set; }
        [DataTableColumnIgnore]
        public string NotaFiscal { get; set; }
        [DataTableColumnIgnore]
        public string Serie { get; set; }
        [DataTableColumnIgnore]
        public string StatusConciliacao { get; set; }
        [DataTableColumnIgnore]
        public bool Habilitado { get; set; }
        public bool Selecionado { get; set; }
        [DataTableColumnIgnore]
        public DateTime? DataCadastro { get; set; }
        [DataTableColumnIgnore]
        public DateTime? DataEmissao { get; set; }
        [DataTableColumnIgnore]
        public bool SucessoLeitura => Criticas?.Count > 0;
        [DataTableColumnIgnore]
        public int LinhaNotaFiscal { get; set; }
        [DataTableColumnIgnore]
        public bool DataEmissaoDivergente { get; set; }
        [DataTableColumnIgnore]
        public bool NotaDivergente { get; set; }
        [DataTableColumnIgnore]
        public List<FaturaArquivoCriticaDTO> Criticas { get; set; } = new List<FaturaArquivoCriticaDTO> { };
        [DataTableColumnIgnore]
        public int FaturaArquivoId { get; set; }
        [DataTableColumnIgnore]
        public string ChaveCte { get; set; }
    }
}
