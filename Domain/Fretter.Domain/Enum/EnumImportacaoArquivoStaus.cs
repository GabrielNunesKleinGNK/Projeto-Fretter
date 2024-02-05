using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Enum
{
    public enum EnumImportacaoArquivoStatus
    {
        Pendente = 1,
        Processando = 2,
        Concluido = 3, 
        Falha = 4,
        NaoClassificado = 5
    }
}
