using Fretter.Domain.Interfaces.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class ERastreamentoTransportadores : IErro
    {
        public int Cd_Id { get; set; }
        public int? Id_Arquivo { get; set; }
        public DateTime? Dt_Ocorrencia { get; set; }
        public int? Id_Ocorrencia { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public DateTime? Dt_Inclusao { get; set; }
        public Guid? Id_Edi { get; set; }
        public Guid? Id_Processo { get; set; }
        public string Cd_CnpjTransportador { get; set; }
        public string Cd_CnpjCanal { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_SerieNotaFiscal { get; set; }
        public string Ds_Complemento { get; set; }
        public string Ds_Erro { get; set; }
        public string Cd_Sro { get; set; }
        public byte? Id_TipoServico { get; set; }
        public int? Nr_Linha { get; set; }
        public string Ds_Json { get; set; }
        public string Cd_BaseTransportador { get; set; }
    }
}
