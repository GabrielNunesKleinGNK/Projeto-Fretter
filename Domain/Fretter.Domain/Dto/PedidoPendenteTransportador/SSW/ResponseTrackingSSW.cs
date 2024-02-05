using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.PedidoPendenteTransportador.SSW
{
    public class ResponseTrackingSSW
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Documento Documento { get; set; }
    }

    public class Documento
    {
        public Header Header { get; set; }
        public List<Tracking> Tracking { get; set; }
    }

    public class Header
    {
        public string Remetente { get; set; }
        public string Destinatario { get; set; }
        public string Nro_Nf { get; set; }
        public string Pedido { get; set; }
    }

    public class Tracking
    {
        public DateTime Data_Hora { get; set; }
        public string Dominio { get; set; }
        public string Filial { get; set; }
        public string Cidade { get; set; }
        public string Ocorrencia { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public DateTime Data_Hora_Efetiva { get; set; }
        public string Nome_recebedor { get; set; }
        public string Nro_Doc_Recebedor { get; set; }
    }
}
