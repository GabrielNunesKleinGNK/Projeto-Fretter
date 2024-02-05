using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Enum
{
    public enum EnumEntregaDevolucaoStatus
    {
        EmAberto = 1,
        AgProcessamento = 2,
        Autorizado = 3,
        NaoAutorizado = 4,
        Rejeitado = 5,
        AgCancelamento = 6,
        Cancelado = 7,
        Erro = 8
    }
}
