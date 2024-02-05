using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ArquivoCobrancaDocumentoViewModel : IViewModel<ArquivoCobrancaDocumento>
    {
		public int Id { get; set; }
        public int ArquivoCobrancaId { get; set; }
        public string FilialEmissora { get; set; }
        public int Tipo { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoCobranca { get; set; }
        public string CFOP { get; set; }
        public string CodigoAcessoNFe { get; set; }
        public string ChaveAcessoNFe { get; set; }
        public string ProtocoloNFe { get; set; }

        public ArquivoCobrancaDocumento Model()
		{
			return new ArquivoCobrancaDocumento(Id, ArquivoCobrancaId, FilialEmissora, Tipo, Serie, Numero, DataEmissao, DataVencimento, ValorTotal, TipoCobranca,
                CFOP, CodigoAcessoNFe, ChaveAcessoNFe, ProtocoloNFe);
		}
    }
}      
