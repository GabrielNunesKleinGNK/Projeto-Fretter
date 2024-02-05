using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe
{
    public class EmpresaIntegracaoItemDetalheFiltro
    {
        public int? TransportadorId { get; set; }
        public int? CanalId { get; set; }
        public string Descricao { get; set; }
        public int? OcorrenciaId { get; set; }
        public string Sigla { get; set; }
        public DateTime DataEnvioInicio { get; set; }
        public DateTime DataEnvioFim { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public int? EmpresaId { get; set; }
        public int Pagina { get; set; } = 0;
        public int PaginaLimite { get; set; } = 10;
        public bool  Sucesso { get; set; }


        public DateTime? DataOcorrenciaInicio { get; set; }
        public DateTime? DataOcorrenciaFim { get; set; }
    }
}
