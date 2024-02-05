using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageRemetente : EntityBase
    {
		#region "Construtores"
		public EntregaStageRemetente (int Id,int EntregaStage,string Nome,string CodigoIntegracao,string Cep,string Endereco,string Numero,string Complemento,string Bairro,string Cidade,string UF,DateTime? Alteracao)
		{
			this.Ativar();
			this.Id = Id;
			this.EntregaStage = EntregaStage;
			this.Nome = Nome;
			this.CodigoIntegracao = CodigoIntegracao;
			this.Cep = Cep;
			this.Endereco = Endereco;
			this.Numero = Numero;
			this.Complemento = Complemento;
			this.Bairro = Bairro;
			this.Cidade = Cidade;
			this.UF = UF;
			this.Alteracao = Alteracao;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
        public int EntregaStage { get; protected set; }
        public string Nome { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string Cep { get; protected set; }
        public string Endereco { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string UF { get; protected set; }
        public DateTime? Alteracao { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarEntregaStage(int EntregaStage) => this.EntregaStage = EntregaStage;
		public void AtualizarNome(string Nome) => this.Nome = Nome;
		public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
		public void AtualizarCep(string Cep) => this.Cep = Cep;
		public void AtualizarEndereco(string Endereco) => this.Endereco = Endereco;
		public void AtualizarNumero(string Numero) => this.Numero = Numero;
		public void AtualizarComplemento(string Complemento) => this.Complemento = Complemento;
		public void AtualizarBairro(string Bairro) => this.Bairro = Bairro;
		public void AtualizarCidade(string Cidade) => this.Cidade = Cidade;
		public void AtualizarUF(string UF) => this.UF = UF;
		public void AtualizarAlteracao(DateTime? Alteracao) => this.Alteracao = Alteracao;
		#endregion
    }
}      
