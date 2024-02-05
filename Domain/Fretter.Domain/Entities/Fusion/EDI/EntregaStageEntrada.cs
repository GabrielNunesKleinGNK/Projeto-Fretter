using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageEntrada : EntityBase
    {
		#region "Construtores"
		public EntregaStageEntrada (int Id,DateTime Inclusao,Guid? Hash,string EntregaSaida,string Json,bool? Validada,int? EntregaStage)
		{
			this.Ativar();
			this.Id = Id;
			this.Inclusao = Inclusao;
			this.Hash = Hash;
			this.EntregaSaida = EntregaSaida;
			this.Json = Json;
			this.Validada = Validada;
			this.EntregaStage = EntregaStage;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
        public DateTime Inclusao { get; protected set; }
        public Guid? Hash { get; protected set; }
        public string EntregaSaida { get; protected set; }
        public string Json { get; protected set; }
        public bool? Validada { get; protected set; }
        public int? EntregaStage { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		public void AtualizarHash(Guid? Hash) => this.Hash = Hash;
		public void AtualizarEntregaSaida(string EntregaSaida) => this.EntregaSaida = EntregaSaida;
		public void AtualizarJson(string Json) => this.Json = Json;
		public void AtualizarValidada(bool? Validada) => this.Validada = Validada;
		public void AtualizarEntregaStage(int? EntregaStage) => this.EntregaStage = EntregaStage;
		#endregion
    }
}      
