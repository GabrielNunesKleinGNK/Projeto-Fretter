using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public partial class EntradaTrackingSequoia
    {
        public long Nrt { get; set; }
        public string Ar { get; set; }
        public string Pedido { get; set; }
        public string NumNfe { get; set; }
        public string NumSerie { get; set; }
        public string ChaveNfe_Cte { get; set; }
        public List<Evento> Eventos { get; set; }
    }

    public partial class Evento
    {
        public string Code { get; set; }
        public long CarrierCode { get; set; }
        public PayLoad PayLoad { get; set; }
    }

    public partial class PayLoad
    {
        public string Date { get; set; }
        public string Comment { get; set; }
        public string Complement { get; set; }
    }
}
