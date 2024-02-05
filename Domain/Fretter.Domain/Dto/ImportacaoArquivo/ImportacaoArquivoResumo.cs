using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.ImportacaoArquivo
{
    public class ImportacaoArquivoResumo
    {
        public int Total { get; set; }
        public int Concluido { get; set; }
	    public int Pendente { get; set; }
	    public int Falha { get; set; }
	    public int Critica { get; set; }

    }
}
