using System;

namespace Fretter.Domain.Entities
{
    public class UsuarioTipo : EntityBase
    {
		#region "Construtores"
		public UsuarioTipo (int Id,string Descricao)
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
		public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
		#endregion
    }
}      
