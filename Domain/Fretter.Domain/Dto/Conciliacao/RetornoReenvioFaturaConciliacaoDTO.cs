using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class RetornoReenvioFaturaConciliacaoDTO
    {
        public RetornoReenvioFaturaConciliacaoDTO()
        {
            Sucesso = false;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set;}
        public DateTime DataReenvio { get; set; }
    }
}
