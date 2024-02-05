using System.Collections.Generic;

namespace Fretter.Domain.Dto.Fretter.Conciliacao
{
    public class ContratoTransportadorRegraDTO
    {
        public List<int> ocorrenciasListId { get; set; }
        public byte tipo { get; set; }
        public bool operacao { get; set; }
        public decimal valor { get; set; }
    }
}
