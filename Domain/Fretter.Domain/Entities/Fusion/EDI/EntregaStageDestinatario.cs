using Fretter.Domain.Dto.Carrefour.Mirakl;
using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageDestinatario : EntityBase
    {
        #region "Construtores"
        public EntregaStageDestinatario (int Id,int EntregaStage,string Nome,string CodigoIntegracao,string CpfCnpj,string InscricaoEstadual,string Cep,string Endereco,string Numero,string Complemento,string Bairro,string Cidade,string UF,DateTime? Alteracao)
		{
			this.Ativar();
			this.Id = Id;
			this.EntregaStage = EntregaStage;
			this.Nome = Nome;
			this.CodigoIntegracao = CodigoIntegracao;
			this.CpfCnpj = CpfCnpj;
			this.InscricaoEstadual = InscricaoEstadual;
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

        public EntregaStageDestinatario(OrderCustomerDTO customer)
        {
			this.AtualizarNome($"{customer.firstname} {customer.lastname}");
			this.AtualizarCodigoIntegracao(customer.customer_id);
			this.AtualizarCep(customer.shipping_address.zip_code);
			this.AtualizarEndereco($"{customer.shipping_address.street_1} {customer.shipping_address.street_2}");
			this.AtualizarCidade(customer.shipping_address.city);
			this.AtualizarUF(customer.shipping_address.state);
        }
        #endregion

        #region "Propriedades"
        public int EntregaStage { get; protected set; }
        public string Nome { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string CpfCnpj { get; protected set; }
        public string InscricaoEstadual { get; protected set; }
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
		public void AtualizarCpfCnpj(string CpfCnpj) => this.CpfCnpj = CpfCnpj;
		public void AtualizarInscricaoEstadual(string InscricaoEstadual) => this.InscricaoEstadual = InscricaoEstadual;
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
