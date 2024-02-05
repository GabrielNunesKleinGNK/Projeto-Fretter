using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fusion.EntregaOcorrencia
{
    public class EntregaEmAbertoDto
    {
        public EntregaEmAbertoDto()
        {

        }

        public int Id { get; set; }
        public string CodigoEntrega { get; set; }
        public int CanalId { get; set; }
        public string Canal { get; set; }
        public int TransportadorId { get; set; }
        public string Transportador { get; set; }
        public string Nota { get; set; }
        public string Serie { get; set; }
        public int? DiasSemAlteracao { get; set; }
        public DateTime? DataUltimaOcorrencia { get; set; }
        public string UltimaOcorrencia { get; set; }
        public int Total { get; set; }
    }
}
