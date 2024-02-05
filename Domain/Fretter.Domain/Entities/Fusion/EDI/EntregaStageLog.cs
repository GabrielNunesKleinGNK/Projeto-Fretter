using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageLog : EntityBase
    {
		#region "Construtores"
		public EntregaStageLog (int Id,DateTime Inclusao,Guid? Hash,string Log,string Exception,string Complemento,string Referencia,string IP,string URL,string Verbo,string Requisicao)
		{
			this.Ativar();
			this.Id = Id;
			this.Inclusao = Inclusao;
			this.Hash = Hash;
			this.Log = Log;
			this.Exception = Exception;
			this.Complemento = Complemento;
			this.Referencia = Referencia;
			this.IP = IP;
			this.URL = URL;
			this.Verbo = Verbo;
			this.Requisicao = Requisicao;
			this.UsuarioCadastro = UsuarioCadastro;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
        public DateTime Inclusao { get; protected set; }
        public Guid? Hash { get; protected set; }
        public string Log { get; protected set; }
        public string Exception { get; protected set; }
        public string Complemento { get; protected set; }
        public string Referencia { get; protected set; }
        public string IP { get; protected set; }
        public string URL { get; protected set; }
        public string Verbo { get; protected set; }
        public string Requisicao { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		public void AtualizarHash(Guid? Hash) => this.Hash = Hash;
		public void AtualizarLog(string Log) => this.Log = Log;
		public void AtualizarException(string Exception) => this.Exception = Exception;
		public void AtualizarComplemento(string Complemento) => this.Complemento = Complemento;
		public void AtualizarReferencia(string Referencia) => this.Referencia = Referencia;
		public void AtualizarIP(string IP) => this.IP = IP;
		public void AtualizarURL(string URL) => this.URL = URL;
		public void AtualizarVerbo(string Verbo) => this.Verbo = Verbo;
		public void AtualizarRequisicao(string Requisicao) => this.Requisicao = Requisicao;
		#endregion
    }
}      
