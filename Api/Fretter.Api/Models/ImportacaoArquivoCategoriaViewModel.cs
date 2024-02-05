using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoArquivoCategoriaViewModel : IViewModel<ImportacaoArquivoCategoria>
    {
		public int Id { get; set; }
        public string Nome { get; set; }
        public int ImportacaoArquivoTipoId { get; set; }
        public string Codigo { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
		public ImportacaoArquivoCategoria Model()
		{
			return new ImportacaoArquivoCategoria(Id,Nome,ImportacaoArquivoTipoId,Codigo);
		}
    }
}      
