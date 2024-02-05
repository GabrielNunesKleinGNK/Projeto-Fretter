using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class JsonIntegracaoFaturaConciliacaoDTO
    {
        public JsonIntegracaoFaturaConciliacaoDTO()
        {
            JsonEnvio = string.Empty;
            JsonRetorno = string.Empty;
        }
        public int EmpresaIntegracaoItemDetalheId { get; set; }
        public string JsonEnvio { get; set; }
        public string JsonRetorno { get; set; }
    }
}
