using System;

namespace Fretter.Domain.Entities
{
    public class EntregaConfiguracaoTipo : EntityBase
    {
		#region "Construtores"
		public EntregaConfiguracaoTipo (int Id,string Tipo)
		{
			this.Ativar();
			this.Id = Id;
			this.Tipo = Tipo;
		}
		#endregion

		#region "Propriedades"
        public string Tipo { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarTipo(string Tipo) => this.Tipo = Tipo;
		#endregion
    }
}      
