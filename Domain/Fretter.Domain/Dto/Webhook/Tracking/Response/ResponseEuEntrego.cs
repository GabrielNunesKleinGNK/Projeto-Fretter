using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Response
{   
    public class ErrorEuEntrego
    {
        public string type { get; set; }
        public string message { get; set; }
    }

    public class ResponseEuEntrego
    {
        public string message { get; set; }
        public List<ErrorEuEntrego> errors { get; set; }
    }
}
