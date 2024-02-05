using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class EntregaDevolucaoDto
    {
        public int EntregaId { get;  set; }
        public int EntregaTransporteTipoId { get;  set; }
        public string CodigoColeta { get;  set; }
        public string CodigoRastreio { get;  set; }
        public DateTime? Validade { get;  set; }
        public string Observacao { get;  set; }
        public DateTime? Inclusao { get;  set; }
        public int EntregaDevolucaoStatus { get;  set; }
        public bool? Processado { get;  set; }
        public bool? Finalizado { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
