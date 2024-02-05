using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class WebhookSyncMessage
    {
        public Guid Ds_Hash { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public string Cd_Ocorrencia { get; set; }
        public DateTime? Dt_Ocorrencia { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_SerieNotaFiscal { get; set; }
        public string Cd_CnpjCanal { get; set; }
        public string Cd_Sro { get; set; }
        public DateTime Dt_Inclusao { get; set; }
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// Lista de Objeto Webhook Sync
    /// </summary>
    public class LsWebhookSync : List<WebhookSyncMessage>
    {
        private int _maxItensPerConnection;

        public bool Enviar => _maxItensPerConnection == Count || this.Any(x => DateTime.Now.Subtract(x.Dt_Inclusao).Minutes >= 1);

        public LsWebhookSync(int maxItensPerConnection) => _maxItensPerConnection = maxItensPerConnection;
    }
}
