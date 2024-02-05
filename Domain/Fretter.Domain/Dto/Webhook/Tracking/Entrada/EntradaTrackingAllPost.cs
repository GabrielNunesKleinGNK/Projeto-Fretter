using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public partial class EntradaTrackingAllPost
    {
        public string tipo { get; set; }
        public DadosAllPost dados { get; set; }
    }

    public partial class DadosAllPost
    {
        public string idPedido { get; set; }
        public string numeroPedido { get; set; }
        public string pedidoAuxiliar { get; set; }
        public string idCotacao { get; set; }
        public string canal { get; set; }
        public string plataforma { get; set; }
        public string transportadora { get; set; }
        public string metodoEnvio { get; set; }
        public long idOcoren { get; set; }
        public string situacao { get; set; }
        public string mensagem { get; set; }
        public string infAdicional { get; set; }
        public DateTime data { get; set; }
    }
}
