using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Webhook
{
    public interface IErro
    {
        string Ds_Erro { get; set; }
        int? Nr_Linha { get; set; }
        string Ds_Json { get; set; }
    }
}
