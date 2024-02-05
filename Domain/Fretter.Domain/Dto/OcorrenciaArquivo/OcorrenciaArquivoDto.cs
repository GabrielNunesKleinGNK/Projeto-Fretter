using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.OcorrenciaArquivo
{
    public class OcorrenciaArquivoDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string NomeArquivo { get; set; }
        public string Url { get; set; }
    }
}
