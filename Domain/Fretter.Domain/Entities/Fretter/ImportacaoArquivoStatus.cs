using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ImportacaoArquivoStatus : EntityBase
    {
		#region "Construtores"
		public ImportacaoArquivoStatus (int Id,string Nome)
		{
			this.Ativar();
			this.Id = Id;
			this.Nome = Nome;
		}
		#endregion

		#region "Propriedades"
        public string Nome { get; protected set; }
		public virtual List<ImportacaoArquivo> ImportacaoArquivos { get; set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarNome(string Nome) => this.Nome = Nome;
		#endregion
    }
}      
