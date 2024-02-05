using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class ContratoTransportadorRegraFiltroDTO
    {
        public int? TransportadorId { get; set; }
        public int? ConciliacaoTipoId { get; set; }
        public int? OcorrenciaEmpresaItemId { get; set; }        
    }
}
