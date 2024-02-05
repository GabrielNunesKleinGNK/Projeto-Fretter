using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Config
{
    public class PedidoIntegracaoConfig
    {
        public List<Integracao> Itens { get; set; }
    }

    public class Integracao
    {
        public string Endpoint { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public EnumPedidoIntegracao PedidoPendenteIntegracaoId { get; set; }
    }
}
