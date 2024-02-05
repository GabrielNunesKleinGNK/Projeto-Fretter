using System;

namespace Fretter.Domain.Entities
{
    public class TabelaCorreioCanalTabelaTipo : EntityBase
    {
		#region "Construtores"
		public TabelaCorreioCanalTabelaTipo (int Id,int TabelaCorreioCanalId, int TabelaTipoId, DateTime Inclusao)
		{			
			this.Id = Id;
			this.TabelaCorreioCanalId = TabelaCorreioCanalId;
			this.TabelaTipoId = TabelaTipoId;
			this.Inclusao = Inclusao;
		}
		#endregion

		#region "Propriedades"
        public int TabelaCorreioCanalId { get; protected set; }
        public int TabelaTipoId { get; protected set; }
        public DateTime Inclusao { get; protected set; }
		#endregion

		#region "Referencias"
		public TabelaTipo TabelaTipo{get; protected set;}
		public TabelaCorreioCanal TabelaCorreioCanal{get; protected set;}
		#endregion

		#region "Métodos"
		public void AtualizarTabelaCorreioCanal(int TabelaCorreioCanal) => this.TabelaCorreioCanalId = TabelaCorreioCanal;
		public void AtualizarTabelaTipo(int TabelaTipo) => this.TabelaTipoId = TabelaTipo;
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		#endregion
    }
}      
