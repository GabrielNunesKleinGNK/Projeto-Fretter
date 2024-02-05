using DocumentFormat.OpenXml.Drawing.Charts;
using Fretter.Domain.Entities;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaConciliacaoIntegracaoDTO
    {
        public int EmpresaIntegracaoItemDetalheId { get; set; }
        public long FaturaConciliacaoId { get; set; }
        public int FaturaId { get; set; }
        public long? ConciliacaoId { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorAdicional { get; set; }
        public DateTime? DataEnvio { get; set; }
        public DateTime? DataProcessamento { get; set; }
        public string HttpStatus { get; set; }
        public bool Sucesso { get; set; }
        public bool Enviado { get; set; }
    }
}
