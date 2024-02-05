using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Response
{
    public partial class ResponseSequoia
    {
        public string Status { get; set; }
        public List<ListaEntregasErros> Lista { get; set; }
    }

    public partial class ListaEntregasErros
    {
        public long Nrt { get; set; }
        public string Status { get; set; }
    }
}
