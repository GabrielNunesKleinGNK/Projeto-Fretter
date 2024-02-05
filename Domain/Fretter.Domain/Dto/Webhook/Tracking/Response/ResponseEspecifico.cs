using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Response
{
    public class ResponseEspecifico
    {
        public string status { get; set; }
        public List<MessageEspecifico> messages { get; set; }
        public string time { get; set; }
        public int? client_id { get; set; }
        public int logistics_provider { get; set; }
        public string logistics_provider_name { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }
        public string hash { get; set; }
    }
    public class MessageEspecifico
    {
        public string type { get; set; }
        public string text { get; set; }
        public string key { get; set; }
    }
}
