using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ImportacaoArquivoTipo : EntityBase
    {
		#region "Construtores"
		public ImportacaoArquivoTipo (int Id,string Nome)
		{
			this.Ativar();
			this.Id = Id;
			this.Nome = Nome;
		}
		#endregion

		#region "Propriedades"
        public string Nome { get; protected set; }
		#endregion

		#region "Referencias"
		public virtual List<ImportacaoConfiguracao> ImportacaoConfiguracoes { get; set; }
		public virtual List<ImportacaoArquivoTipoItem> ImportacaoArquivoTipoItems { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarNome(string Nome) => this.Nome = Nome;
		#endregion
    }
}      
