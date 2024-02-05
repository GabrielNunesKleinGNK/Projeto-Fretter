using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Message
{
    public class TrackingEspecificoMessage
    {
        public int Cd_Linha { get; set; }
        public Guid Ds_Hash { get; set; }
        public int? Id_Empresa { get; set; }
        public int? Id_Transportador { get; set; }
        public byte? Id_OrigemImportacao { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public string Ds_Complementar { get; set; }
        public string Cd_Ocorrencia { get; set; }
        public string Cd_BaseTransportador { get; set; }
        public DateTime? Dt_Ocorrencia { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_SerieNotaFiscal { get; set; }
        public string Cd_CnpjCanal { get; set; }
        public string Cd_CnpjTransportador { get; set; }
        public string Cd_Sro { get; set; }
        public string Cd_Danfe { get; set; }
        public string Cd_EntregaSaida { get; set; }
        public string Ds_Dominio { get; set; }
        public string Cd_Latitude { get; set; }
        public string Cd_Longitude { get; set; }
        public string Nm_Cidade { get; set; }
        public string Cd_Uf { get; set; }
        public DateTime Dt_Inclusao { get; set; }
        public Exception Exception { get; set; }
    }

    public class LsTrackingEspecificoMessage : List<TrackingEspecificoMessage>
    {
        private int _maxItensPerConnection;

        public bool Enviar => _maxItensPerConnection == Count || this.Any(x => DateTime.Now.Subtract(x.Dt_Inclusao).Minutes >= 1);

        public LsTrackingEspecificoMessage(int maxItensPerConnection) => _maxItensPerConnection = maxItensPerConnection;
    }
}
