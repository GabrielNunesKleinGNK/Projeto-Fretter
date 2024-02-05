using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class EntregaConciliacaoInsereDTO
    {
        public Int64 Id_EntregaConciliacao { get; set; }
        public Int64 Id_Entrega { get; set; }
        public decimal? Vl_Cobrado { get; set; }
        public decimal? Vl_Altura { get; set; }
        public decimal? Vl_Largura { get; set; }                        
        public decimal? Vl_Comprimento { get; set; }
        public decimal? Vl_Diametro { get; set; }
        public decimal? Vl_Peso { get; set; }
        public decimal? Vl_Cubagem { get; set; }
        public string Ds_Json { get; set; }
        public DateTime? Dt_Processamento { get; set; }
        public string Ds_RetornoProcessamento { get; set; }
        public bool? Flg_Sucesso { get; set; }
    }
}
