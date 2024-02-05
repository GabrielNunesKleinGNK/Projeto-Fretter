using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.ImportacaoArquivo
{
    public class ImportacaoArquivoFiltro
    {
        public DateTime StartDataCadastro { get; set; }
        public DateTime EndDataCadastro { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public int TransportadorId { get; set; }
        public int ImportacaoArquivoStatusId { get; set; }
    }
}
