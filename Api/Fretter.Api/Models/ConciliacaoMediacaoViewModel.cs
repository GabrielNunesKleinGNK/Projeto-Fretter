using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConciliacaoMediacaoViewModel : IViewModel<ConciliacaoMediacao>
    {
		public int Id { get; set; }
        public int ImportacaoArquivoId { get; set; }
        public int ImportacaoArquivoCategoriaId { get; set; }
        public int? TipoServico { get; set; }
        public string Chave { get; set; }
        public string Codigo { get; set; }
        public string Numero { get; set; }
        public int? DigitoVerificador { get; set; }
        public string Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? ValorPrestacaoServico { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
		public ConciliacaoMediacao Model()
		{
			return new ConciliacaoMediacao(Id,ImportacaoArquivoId,ImportacaoArquivoCategoriaId,TipoServico,Chave,Codigo,Numero,DigitoVerificador,Serie,DataEmissao,ValorPrestacaoServico);
		}
    }
}      
