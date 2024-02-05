using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Enum
{
    public enum EnumEntregaConfiguracaoTipo
    {
        Ativo = 1,
        CallBackUrl = 2,
        Vtex = 3,
        Anymarket = 4,
        GerenciarIncidenteMirakl = 5
    }

    public enum EnumEntregaConfiguracaoIntervaloExecucaoTipo
    {
        Minuto = 1,
        Horas = 2,
        Dias = 3
    }

    public enum EnumEntregaConfiguracaoItemTipo
    {
        ListaSkuVtex = 1,
        CadastroSkuVtex = 2,
        DadosSellerVtex = 3,
        SkusAnymarket = 4,
        AberturaIncidente = 5,
        FechamentoIncidente = 6
    }
}
