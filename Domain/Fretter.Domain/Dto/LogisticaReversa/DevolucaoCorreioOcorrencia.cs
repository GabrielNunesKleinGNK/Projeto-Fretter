using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.LogisticaReversa
{
    public class DevolucaoCorreioOcorrencia
    {
        public int Id_EntregaDevolucao { get; set; }
        public long Id_Entrega { get; set; }
        public string Cd_CodigoIntegracao { get; set; }
        public string Nm_Sigla { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public string Ds_Observacao { get; set; }
        public DateTime? Dt_Ocorrencia { get; set; }
        public string Cd_CodigoColeta { get; set; }
        public string Cd_CodigoRastreio { get; set; }
        public string Vl_PesoCubico { get; set; }
        public string Vl_PesoReal { get; set; }
        public string Vl_Postagem { get; set; }

    }
}
