using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public class EntradaTrackingFreteRapido
    {
        public string Id_Frete { get; set; }
        public string Numero_Pedido { get; set; }
        public long Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Ocorrencia { get; set; }
        public DateTime Data_Atualizacao { get; set; }
        public DateTime Data_Reentrega { get; set; }
        public string Prazo_Devolucao { get; set; }
        public string Mensagem { get; set; }
        public List<NotaFiscal> Notas_Fiscais { get; set; }
    }

    public partial class NotaFiscal
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string Chave_Acesso { get; set; }
    }
}
