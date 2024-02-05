using System;
using System.Collections.Generic;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoArquivoViewModel : IViewModel<ImportacaoArquivo>
    {
		public int Id { get; set; }
        public string Nome { get; set; }
        public int? EmpresaId { get; set; }
        public int ImportacaoArquivoTipoId { get; set; }
        public int ImportacaoArquivoStatusId { get; set; }
        public string Identificador { get; set; }
        public string Diretorio { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public ImportacaoArquivoStatusViewModel ImportacaoArquivoStatus { get; set; }
        public ImportacaoArquivoTipoViewModel ImportacaoArquivoTipo { get; set; }
        public List<ImportacaoArquivoCriticaViewModel> ImportacaoArquivoCriticas { get; set; }
        public ImportacaoArquivo Model()
		{
			return new ImportacaoArquivo(Id,Nome,EmpresaId,ImportacaoArquivoTipoId, ImportacaoArquivoStatusId, Identificador,Diretorio);
		}
    }
}      
