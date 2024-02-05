using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Response
{   
    public class ErrorLoggi
    {
        public string type { get; set; }
        public string message { get; set; }
    }

    public class ResponseLoggi
    {
        public string message { get; set; }
        public List<ErrorLoggi> errors { get; set; }
    }
}
