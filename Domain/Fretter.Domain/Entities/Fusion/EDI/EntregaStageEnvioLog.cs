using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageEnvioLog : EntityBase
    {
		#region "Construtores"

		public EntregaStageEnvioLog() { }
		public EntregaStageEnvioLog(int Id, int EntregaConfiguracaoId, string CodigoIntegracao, string EntregaSaida, string EntregaEntrada, DateTime Processamento,string Json, string Retorno, bool Sucesso, bool Processado, bool Ativo)
		{
			this.Ativar();
			this.Id = Id;
			this.EntregaConfiguracaoId = EntregaConfiguracaoId;
			this.CodigoIntegracao = CodigoIntegracao;
			this.EntregaEntrada = EntregaEntrada;
			this.EntregaSaida = EntregaSaida;
			this.Processamento = Processamento;
			this.Json = Json;
			this.Retorno = Retorno;
			this.Sucesso = Sucesso;
			this.Processado = Processado;
			this.Ativo = Ativo;
		}
        #endregion

        #region "Propriedades"
        public int EntregaConfiguracaoId { get; set; }
		public string CodigoIntegracao { get; protected set; }
		public string EntregaEntrada { get; protected set; }
		public string EntregaSaida { get; protected set; }
		public DateTime Processamento { get; protected set; }
		public string Json { get; protected set; }
		public string Retorno { get; protected set; }
		public bool? Sucesso { get; protected set; }
		public bool? Processado { get; protected set; }

		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarEntregaConfiguracaoId(int EntregaConfiguracaoId) => this.EntregaConfiguracaoId = EntregaConfiguracaoId;
		public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
		public void AtualizarEntregaEntrada(string EntregaEntrada) => this.EntregaSaida = EntregaEntrada;
		public void AtualizarEntregaSaida(string EntregaSaida) => this.EntregaSaida = EntregaSaida;
		public void AtualizarCodidoDasEntregas(string CodigoIntegracao)
		{
			this.CodigoIntegracao = CodigoIntegracao;
			this.EntregaEntrada = CodigoIntegracao;
			this.EntregaSaida = CodigoIntegracao;
		}
		public void AtualizarProcessamento(DateTime Processamento) => this.Processamento = Processamento;
		public void AtualizarJson(string Json) => this.Json = Json;
		public void AtualizarRetorno(string Retorno) => this.Retorno = Retorno;
		public void AtualizarSucesso(bool? Sucesso) => this.Sucesso = Sucesso;
		public void AtualizarProcessado(bool? Processado) => this.Processado = Processado;

		#endregion
    }
}      
