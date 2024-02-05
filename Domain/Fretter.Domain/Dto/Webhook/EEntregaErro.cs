using Fretter.Domain.Interfaces.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class EEntregaErro : IErro
    {
        public int Cd_Id { get; set; }
        public int? Id_Arquivo { get; set; }
        public int? Nr_Linha { get; set; }
        public string Ds_Erro { get; set; }
        public int? Id_Empresa { get; set; }
        public int? Id_Transportador { get; set; }
        public int? Id_EmpresaMarketplace { get; set; }
        public string Ds_Json { get; set; }
        public string Cd_Entrega { get; set; }
        public string Cd_Sro { get; set; }
        public int? Id_Lojista { get; set; }
    }
}
