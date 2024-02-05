using System;
namespace Fretter.Domain.Dto.ArquivoImportacaoLog
{
    public class ArquivoImportacaoLogFiltro
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string ProcessType { get; set; }
        public string RequestNumber { get; set; }

    }
}
