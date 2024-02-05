using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.LogCotacaoFrete
{
    public class Data
    {
        public int bodyLength { get; set; }
        public string body { get; set; }
        public List<string> header { get; set; }
    }

    public class MessageObject
    {
        public string uri { get; set; }
        public string action { get; set; }
        public string instancia { get; set; }
        public string parameters { get; set; }
        public string delivery_not_available { get; set; }
        public int empresaId { get; set; }
        public double peso { get; set; }
        public Data data { get; set; }
    }

    public class LogCotacaoFreteMessage
    {
        public DateTime timeStamp { get; set; }
        public string dsLog { get; set; }
        public string dsProcesso { get; set; }
        public string hash { get; set; }
        public string loggerName { get; set; }
        public string level { get; set; }
        public string hostName { get; set; }
        public MessageObject messageObject { get; set; }
        public string message { get; set; }
    }


}
