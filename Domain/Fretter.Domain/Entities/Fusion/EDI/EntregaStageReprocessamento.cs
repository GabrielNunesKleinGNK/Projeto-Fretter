using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageReprocessamento : EntityBase
    {
		#region "Construtores"
		public EntregaStageReprocessamento(int Id, string Entrega, DateTime Inclusao, bool Processado, DateTime? DataProcessamento, string JsonEnviadoParaFila, Enum.EnumEntregaStageStatusProcessamento EntregaStageStatusProcessamentoId)
		{
			this.Ativar();
			this.Id = Id;
			this.Entrega = Entrega;
			this.Inclusao = Inclusao;
			this.Processado = Processado;
			this.DataProcessamento = DataProcessamento;
			this.JsonEnviadoParaFila = JsonEnviadoParaFila;
			this.EntregaStageStatusProcessamentoId = EntregaStageStatusProcessamentoId;
		}
		#endregion

		#region "Propriedades"
        public string Entrega { get; protected set; }
        public DateTime Inclusao { get; protected set; }
		public bool Processado { get; protected set; }
		public DateTime? DataProcessamento { get; protected set; }
        public string JsonEnviadoParaFila { get; protected set; }
        public Enum.EnumEntregaStageStatusProcessamento EntregaStageStatusProcessamentoId { get; set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarEntrega(string Entrega) => this.Entrega = Entrega;
		public void AtualizarAlteracao(DateTime Inclusao) => this.Inclusao = Inclusao;
		public void AtualizarProcessado(bool Processado) => this.Processado = Processado;
		public void AtualizarDataProcessamento(DateTime? DataProcessamento) => this.DataProcessamento = DataProcessamento;
		public void AtualizarJsonEnviadoParaFila(string JsonEnviadoParaFila) => this.JsonEnviadoParaFila = JsonEnviadoParaFila;
		public void AtualizarEntregaStageStatusProcessamentoId(Enum.EnumEntregaStageStatusProcessamento EntregaStageStatusProcessamentoId) => this.EntregaStageStatusProcessamentoId = EntregaStageStatusProcessamentoId;
		#endregion
	}
}      
