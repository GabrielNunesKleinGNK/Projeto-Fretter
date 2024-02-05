using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoCteNotaFiscalViewModel : IViewModel<ImportacaoCteNotaFiscal>
    {
        public int Id { get; set; }
        public int ImportacaoCteId { get; set; }
        public string Chave { get; set; }
        public string CNPJEmissorNF { get; set; }
        public string NumeroNF { get; set; }
        public string SerieNF { get; set; }
        public DateTime? DataEmissaoNF { get; set; }

        public ImportacaoCteNotaFiscal Model()
        {
            return new ImportacaoCteNotaFiscal(Id, ImportacaoCteId, Chave, null);
        }
    }
}
