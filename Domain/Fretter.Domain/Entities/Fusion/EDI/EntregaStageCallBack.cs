using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities
{
    public class EntregaStageCallBack : EntityBase
    {
        public EntregaStageCallBack() { }

        public string Cd_EntregaSaida { get; set; }
        public string Ds_LinkEtiquetaPDF { get; set; }
        public DateTime? Dt_ValidadeFimEtiqueta { get; set; }
        public string Ds_NomeTransportador { get; set; }
        public string Cd_Sro { get; set; }
        public List<EntregaStageCallBackErros> Erros { get; set; }
        public List<string> Cd_EntregaSaidaItens { get; set; }
        public bool? Flg_ContemIncidente { get; set; }
        public bool? Flg_NovoEnvio { get; set; }
		public int Id_EmpresaMarketplace { get; set; }
	}

    public class EntregaStageCallBackErros
    {
        public string Cd_Codigo { get; set; }
        public string Ds_Descricao { get; set; }
    }
}
