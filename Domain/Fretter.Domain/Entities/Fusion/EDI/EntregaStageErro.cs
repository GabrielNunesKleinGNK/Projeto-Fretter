using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageErro : EntityBase
    {
		#region "Construtores"
		public EntregaStageErro (int Id,DateTime Importacao,int? Arquivo,string Retorno,string JsonEntrada,string JsonProcessamento)
		{
			this.Ativar();
			this.Id = Id;
			this.Importacao = Importacao;
			this.Arquivo = Arquivo;
			this.Retorno = Retorno;
			this.JsonEntrada = JsonEntrada;
			this.JsonProcessamento = JsonProcessamento;
			this.Ativo = Ativo;
		}

		public EntregaStageErro(int Id, DateTime Importacao, int? Arquivo, string Retorno, int CodigoErro, string EntregaSaida)
		{
			this.Ativar();
			this.Id = Id;
			this.Importacao = Importacao;
			this.Arquivo = Arquivo;
			this.Retorno = Retorno;
			this.Cd_EntregaSaida = EntregaSaida;
			this.Id_CodigoErro = CodigoErro;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
		public DateTime Importacao { get; protected set; }
        public int? Arquivo { get; protected set; }
        public string Retorno { get; protected set; }
        public string JsonEntrada { get; protected set; }
        public string JsonProcessamento { get; protected set; }

        public int? Id_CodigoErro { get; protected set; }
		public string Cd_EntregaSaida { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarImportacao(DateTime Importacao) => this.Importacao = Importacao;
		public void AtualizarArquivo(int? Arquivo) => this.Arquivo = Arquivo;
		public void AtualizarRetorno(string Retorno) => this.Retorno = Retorno;
		public void AtualizarJsonEntrada(string JsonEntrada) => this.JsonEntrada = JsonEntrada;
		public void AtualizarJsonProcessamento(string JsonProcessamento) => this.JsonProcessamento = JsonProcessamento;
		#endregion
    }
}      
