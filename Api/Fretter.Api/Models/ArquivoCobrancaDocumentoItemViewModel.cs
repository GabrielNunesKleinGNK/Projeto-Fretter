using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ArquivoCobrancaDocumentoItemViewModel : IViewModel<ArquivoCobrancaDocumentoItem>
    {
		public int Id { get; set; }
        public int ArquivoCobrancaDocumentoId { get; set; }
        public string Filial { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public decimal ValorFrete { get; set; }
        public DateTime DataEmissao { get; set; }
        public string DocumentoRemetente { get; set; }
        public string DocumentoDestinatario { get; set; }
        public string DocumentoEmissor { get; set; }
        public string UfEmbarcadora { get; set; }
        public string UfDestinataria { get; set; }
        public string UfEmissora { get; set; }
        public string CodigoIVA { get; set; }
        public bool Devolucao { get; set; }

        public ArquivoCobrancaDocumentoItem Model()
		{
			return new ArquivoCobrancaDocumentoItem(Id, ArquivoCobrancaDocumentoId, Filial, Serie, Numero, DataEmissao, ValorFrete, DocumentoRemetente,
                DocumentoDestinatario, DocumentoEmissor, UfEmbarcadora, UfDestinataria, UfEmissora, Devolucao, CodigoIVA);
		}
    }
}      
