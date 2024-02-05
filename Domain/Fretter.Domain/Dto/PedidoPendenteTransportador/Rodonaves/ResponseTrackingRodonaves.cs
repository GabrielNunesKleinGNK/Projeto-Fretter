using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.PedidoPendenteTransportador.Rodonaves
{
    public class ResponseTrackingRodonaves
    {
        public string SenderDescription { get; set; }
        public string SenderTaxIdRegistration { get; set; }
        public string RecipientDescription { get; set; }
        public string RecipientTaxIdRegistration { get; set; }
        public int ExpectedDeliveryDays { get; set; }
        public string CommercialPhone { get; set; }
        public string CommercialDDD { get; set; }
        public List<Event> Events { get; set; }

    }

    public class Event
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
        public string ProcedaCode { get; set; }
    }
}
