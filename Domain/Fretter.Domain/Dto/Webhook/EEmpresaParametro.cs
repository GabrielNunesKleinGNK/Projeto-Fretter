using Fretter.Domain.Enum.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class EEmpresaParametro
    {
        public int Cd_Id { get; set; }
        public int Id_Empresa { get; set; }
        public Enums.EmpresaParametroTipo Id_EmpresaParametroTipo { get; set; }
        public string Valor { get; set; }
    }
}
