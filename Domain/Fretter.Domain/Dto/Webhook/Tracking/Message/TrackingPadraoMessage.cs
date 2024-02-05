using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Message
{
    public class TrackingPadraoMessage
    {
        public int Cd_Linha { get; set; }
        public Guid Ds_Hash { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public string Ds_Complemento { get; set; }
        public string Cd_Ocorrencia { get; set; }
        public string Dt_Ocorrencia { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_SerieNotaFiscal { get; set; }
        public string Cd_CnpjCanal { get; set; }
        public string Cd_CnpjTransportador { get; set; }
        public string Cd_Sro { get; set; }
        public string Cd_Danfe { get; set; }
        public string Ds_Token { get; set; }
        public DateTime Dt_Inclusao { get; set; }
        public Exception Exception { get; set; }
    }
}
