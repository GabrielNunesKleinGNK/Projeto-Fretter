using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.EntregaAgendamento
{
    public class DisponibilidadeDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string DataCompleta { get; set; }
        public int Manha { get; set; }
        public int Tarde { get; set; }
        public int Noite { get; set; }
        public bool desabilitaNoite => Noite == 0;
        public bool desabilitaManha => Manha == 0;
        public bool desabilitaTarde => Tarde == 0;
        public int? TransportadorId { get; set; }
        public int? TransportadorCnpjId { get; set; }
    }
}
