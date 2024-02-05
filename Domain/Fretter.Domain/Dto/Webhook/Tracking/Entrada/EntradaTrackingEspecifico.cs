using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public class EntradaTrackingEspecifico
    {
        public string logistic_provider { get; set; }
        public int? logistic_provider_id { get; set; }
        public string logistic_provider_federal_tax_id { get; set; }
        public string shipper { get; set; }
        public string shipper_federal_tax_id { get; set; }
        public string invoice_key { get; set; }
        public string invoice_series { get; set; }
        public string invoice_number { get; set; }
        public string tracking_code { get; set; }
        public string order_number { get; set; }
        public string volume_number { get; set; }
        public List<EventEspecifico> events { get; set; }
    }

    public class EventEspecifico
    {
        public DateTimeOffset event_date { get; set; }
        public string original_base { get; set; }
        public string original_code { get; set; }
        public string original_message { get; set; }
        public List<AttachmentEspecifico> attachments { get; set; }
        public LocationEspecifico location { get; set; }
    }

    public class AttachmentEspecifico
    {
        public string url { get; set; }
        public string content_in_base64 { get; set; }
        public string type { get; set; }
        public string file_name { get; set; }
        public string mime_type { get; set; }
        public AdditionalInformationEspecifico additional_information { get; set; }
    }

    public class LocationEspecifico
    {
        public string address { get; set; }
        public string number { get; set; }
        public string additional { get; set; }
        public string reference { get; set; }
        public string city { get; set; }
        public string state_code { get; set; }
        public string quarter { get; set; }
        public string zip_code { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    public class AdditionalInformationEspecifico
    {
        public string nome { get; set; }
        public string rg { get; set; }
    }
}
