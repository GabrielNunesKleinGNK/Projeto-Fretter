using Fretter.Domain.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.LogisticaReversa
{
    public class DevolucaoCorreioRetorno
    {
        public int Id_Empresa { get; set; }
        public int Id_EntregaDevolucao { get; set; }
        public int Id_DevolucaoCorreioRetornoTipo { get; set; }
        public long Id_Entrega { get; set; }
        public string Cd_Pedido { get; set; }
        public string Cd_CodigoColeta { get; set; }
        public string Cd_CodigoRastreio { get; set; }
        public DateTime? Dt_Validade { get; set; }
        public string Ds_Mensagem { get; set; }
        public string Ds_Retorno { get; set; }
        public int Id_EntregaDevolucaoStatus { get; set; }
        public string Cd_CodigoStatusIntegracao { get; set; }
        public bool Flg_InsereOcorrencia { get; set; }
        public bool Flg_Sucesso { get; set; }
        [DataTableColumnIgnore]
        public string Ds_Ocorrencia { get; set; }
        [DataTableColumnIgnore]
        public string Ds_OcorrenciaHistorico { get; set; }
        public string Ds_UrlReversa { get; set; }
        public string Cd_CodigoRetorno { get; set; }
        public string Ds_XmlRetorno { get; set; }
    }
}
