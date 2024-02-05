using System;

namespace Fretter.Domain.Entities
{
    public class EntregaConfiguracaoHistorico : EntityBase
    {
		#region "Construtores"
		public EntregaConfiguracaoHistorico (long Id,int EntregaConfiguracaoId, int? Processado,DateTime? EntregaMinima, 
										     DateTime? EntregaMaxima, DateTime? PeriodoInicial, DateTime? PeriodoFinal,
											 Int64? ControleInicial,Int64? ControleFinal,string MensagemRetorno,bool? Sucesso)
		{
			this.Ativar();
			this.Id = Id;
			this.EntregaConfiguracaoId = EntregaConfiguracaoId;
			this.Processado = Processado;
			this.EntregaMinima = EntregaMinima;
			this.EntregaMaxima = EntregaMaxima;
			this.PeriodoInicial = PeriodoInicial;
			this.PeriodoFinal = PeriodoFinal;
			this.ControleInicial = ControleInicial;
			this.ControleFinal = ControleFinal;
			this.MensagemRetorno = MensagemRetorno;
			this.Sucesso = Sucesso;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
		public long Id { get; protected set; }
		public int EntregaConfiguracaoId { get; protected set; }
        public int? Processado { get; protected set; }
        public DateTime? EntregaMinima { get; protected set; }
        public DateTime? EntregaMaxima { get; protected set; }
        public DateTime? PeriodoInicial { get; protected set; }
        public DateTime? PeriodoFinal { get; protected set; }
        public Int64? ControleInicial { get; protected set; }
        public Int64? ControleFinal { get; protected set; }
        public string MensagemRetorno { get; protected set; }
        public bool? Sucesso { get; protected set; }
		#endregion

		#region "Referencias"
		public virtual EntregaConfiguracao EntregaConfiguracao { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarEntregaConfiguracao(int EntregaConfiguracao) => this.EntregaConfiguracaoId = EntregaConfiguracao;
		public void AtualizarProcessado(int? Processado) => this.Processado = Processado;
		public void AtualizarEntregaMinima(DateTime? EntregaMinima) => this.EntregaMinima = EntregaMinima;
		public void AtualizarEntregaMaxima(DateTime? EntregaMaxima) => this.EntregaMaxima = EntregaMaxima;
		public void AtualizarPeriodoInicial(DateTime? PeriodoInicial) => this.PeriodoInicial = PeriodoInicial;
		public void AtualizarPeriodoFinal(DateTime? PeriodoFinal) => this.PeriodoFinal = PeriodoFinal;
		public void AtualizarControleInicial(Int64? ControleInicial) => this.ControleInicial = ControleInicial;
		public void AtualizarControleFinal(Int64? ControleFinal) => this.ControleFinal = ControleFinal;
		public void AtualizarMensagemRetorno(string MensagemRetorno) => this.MensagemRetorno = MensagemRetorno;
		public void AtualizarSucesso(bool? Sucesso) => this.Sucesso = Sucesso;
		#endregion
    }
}      
