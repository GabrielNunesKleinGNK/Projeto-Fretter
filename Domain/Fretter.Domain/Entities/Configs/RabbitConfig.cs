using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Configs
{
    public class RabbitConfig
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string VirtualHost { get; set; }
        public string Host { get; set; }
    }
}
