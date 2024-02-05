using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Config
{
    public class SeriLogConfig
    {
        public SeriLogConfig()
        {
        }
        public string ConnectionUri { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string IndexPrefix { get; set; }
        public string ApplicationName { get; set; }
        public string Ambiente { get; set; }
    }
}
