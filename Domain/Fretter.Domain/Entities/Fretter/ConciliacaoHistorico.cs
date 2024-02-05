using System;

namespace Fretter.Domain.Entities
{
    public class ConciliacaoHistorico : EntityBase
    {
		#region "Construtores"
		public ConciliacaoHistorico (int Id,Int64? ConciliacaoHistoricoId,Int64 ConciliacaoId,string Descricao)
		{
			this.Ativar();
			this.Id = Id;
			this.ConciliacaoHistoricoId = ConciliacaoHistoricoId;
			this.ConciliacaoId = ConciliacaoId;
			this.Descricao = Descricao;
		}
		#endregion

		#region "Propriedades"
        public Int64? ConciliacaoHistoricoId { get; protected set; }
        public Int64 ConciliacaoId { get; protected set; }
        public string Descricao { get; protected set; }
		#endregion

		#region "Referencias"
		public Conciliacao Conciliacao{get; protected set;}
		#endregion

		#region "Métodos"
		public void AtualizarConciliacaoHistoricoId(Int64? ConciliacaoHistoricoId) => this.ConciliacaoHistoricoId = ConciliacaoHistoricoId;
		public void AtualizarConciliacaoId(Int64 ConciliacaoId) => this.ConciliacaoId = ConciliacaoId;
		public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
		#endregion
    }
}      
