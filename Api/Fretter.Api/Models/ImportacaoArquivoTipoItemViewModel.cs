using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoArquivoTipoItemViewModel : IViewModel<ImportacaoArquivoTipoItem>
    {
		public int Id { get; set; }        
        public int ImportacaoArquivoTipoId { get; set; }
        public int ConciliacaoTipoId { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public ConciliacaoTipoViewModel ConciliacaoTipo { get; set; }
        public ImportacaoArquivoTipoItem Model()
		{
			return new ImportacaoArquivoTipoItem(Id, ImportacaoArquivoTipoId, ConciliacaoTipoId);
		}
    }
}      
