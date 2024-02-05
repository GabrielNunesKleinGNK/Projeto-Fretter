using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.TrackingIntegracao
{
    public class TrackingIntegracaoEntradaDto
    {
        public long? CodigoIntegracaoEntrega { get; set; }
        public long? CodigoIntegracaoControle { get; set; }
        public string Descricao { get; set; }
        public string DescricaoComplementar { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public string Dominio { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

        public int? TransportadorId { get; set; }
        public string TransportadorCnpj { get; set; }
        public string CodigoOcorrencia { get; set; }
        public string Danfe { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public string Sro { get; set; }
        public string CodigoEntrega { get; set; }
        public string CodigoPedido { get; set; }
        public string Mensagem { get; set; }
        public string ProtocoloEntrega { get; set; }
        public string ProtocoloOcorrencia { get; set; }
    }
}
