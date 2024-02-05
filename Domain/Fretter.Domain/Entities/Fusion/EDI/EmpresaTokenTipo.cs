using System;

namespace Fretter.Domain.Entities
{
    public class EmpresaTokenTipo : EntityBase
    {
		#region "Construtores"
		public EmpresaTokenTipo (int Id,string TokenTipo)
		{
			this.Ativar();
			this.Id = Id;
			this.TokenTipo = TokenTipo;
		}
		#endregion

		#region "Propriedades"
        public string TokenTipo { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarTokenTipo(string TokenTipo) => this.TokenTipo = TokenTipo;
		#endregion
    }
}      
