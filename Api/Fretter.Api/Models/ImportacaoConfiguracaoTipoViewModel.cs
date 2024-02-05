using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoConfiguracaoTipoViewModel : IViewModel<ImportacaoConfiguracaoTipo>
    {
		public int Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }
		public ImportacaoConfiguracaoTipo Model()
		{
			return new ImportacaoConfiguracaoTipo(Id,Nome);
		}
    }
}      
