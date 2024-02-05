using System;

namespace Fretter.Domain.Entities
{
    public class Configuracao : EntityBase
    {
		#region "Construtores"
		public Configuracao (int Id,string Chave,string Valor)
		{
			this.Ativar();
			this.Id = Id;
			this.Chave = Chave;
			this.Valor = Valor;
		}
		#endregion

		#region "Propriedades"
        public string Chave { get; protected set; }
        public string Valor { get; protected set; }
		#endregion

		#region "Referencias"
		//public Usuario UsuarioCadastro{get; protected set;}
		////public Usuario UsuarioAlteracao{get; protected set;}
		#endregion

		#region "Métodos"
		public void AtualizarChave(string Chave) => this.Chave = Chave;
		public void AtualizarValor(string Valor) => this.Valor = Valor;
		#endregion
    }
}      
