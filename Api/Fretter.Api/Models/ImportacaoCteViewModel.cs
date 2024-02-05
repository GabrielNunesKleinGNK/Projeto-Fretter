using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoCteViewModel : IViewModel<ImportacaoCte>
    {
		public int Id { get; set; }
        public int? TipoAmbiente { get; set; }
        public int? ImportacaoArquivoId { get; set; }
        public int? TipoCte { get; set; }
        public int? TipoServico { get; set; }
        public string Chave { get; set; }
        public string Codigo { get; set; }
        public string Numero { get; set; }
        public int? DigitoVerificador { get; set; }
        public string Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? ValorPrestacaoServico { get; set; }
        public string JsonComposicaoValores { get; set; }
        public ImportacaoCte Model()
		{
			return new ImportacaoCte(Id, TipoAmbiente, ImportacaoArquivoId, TipoCte, TipoServico, Chave, Codigo, Numero, DigitoVerificador, Serie, DataEmissao, ValorPrestacaoServico, JsonComposicaoValores);
		}
    }
}      
