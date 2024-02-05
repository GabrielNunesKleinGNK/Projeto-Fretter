using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class EntregaEmAbertoFiltro
    {
        public EntregaEmAbertoFiltro()
        {
        }

        public int? EmpresaId { get; set; }
        public int? TransportadorId { get; set; }
        public int? OcorrenciaId { get; set; }
        public int? EmpresaMarketplaceId { get; set; }
        public bool EntregasMarketplace { get; set; }
        //public DateTime DataImportacao { get; set; }
        public DateTime? DataUltimaOcorrencia { get; set; }
        public DateTime? DataUltimaOcorrenciaFim { get; set; }

        public string Pedidos { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int Pagina { get; set; } = 0;
        public int PaginaLimite { get; set; } = 10;
    }
}
