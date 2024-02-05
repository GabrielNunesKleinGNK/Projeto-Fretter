using Fretter.Domain.Interfaces.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class ECriticaArquivo : IErro, IErroOco
    {
        public int Cd_Id { get; set; }
        public int Id_Arquivo { get; set; }
        public DateTime? Dt_Ocorrencia { get; set; }
        public byte Id_CriticaTipo { get; set; }
        public int? Id_Empresa { get; set; }
        public int? Id_Transportador { get; set; }
        public string Ds_Erro { get; set; }
        public int? Nr_Linha { get; set; }
        public string Ds_Json { get; set; }
    }
}
