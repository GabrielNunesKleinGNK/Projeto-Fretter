using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Webhook
{
    public interface IErroOco
    {
        int Cd_Id { get; set; }
        int Id_Arquivo { get; set; }
        DateTime? Dt_Ocorrencia { get; set; }
        byte Id_CriticaTipo { get; set; }
        int? Id_Empresa { get; set; }
        int? Id_Transportador { get; set; }
        string Ds_Json { get; set; }
    }
}
