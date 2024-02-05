using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaAcao : EntityBase
    {
		#region "Construtores"
		public FaturaAcao(int Id,string Descricao)
		{
			this.Ativar();
			this.Id = Id;
			this.Descricao = Descricao;
		}
		#endregion

		#region "Propriedades"
        public string Descricao { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarDescricao(string Descicao) => this.Descricao = Descricao;
		#endregion
    }
}      
