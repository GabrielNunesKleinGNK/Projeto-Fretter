using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class CnpjDetalhe : EntityBase
    {
        #region "Construtores"
        public CnpjDetalhe(int Id, string Cnpj, string Tipo, string Cep, string UF)
        {
            this.Id = Id;
            this.Cnpj = Cnpj;
            this.Tipo = Tipo;
            this.Cep = Cep;
            this.UF = UF;
        }
        #endregion

        #region "Propriedades"
        public string Cnpj { get; protected set; }
        public string Tipo { get; protected set; }
        public DateTime? Abertura { get; protected set; }
        public string Logradouro { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Cep { get; protected set; }
        public string Municipio { get; protected set; }
        public string UF { get; protected set; }
        public string Email { get; protected set; }
        public string Telefone { get; protected set; }
        [NotMapped]
        public Int64? CnpjUnico { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarCnpj(string Cnpj) => this.Cnpj = Cnpj;
        public void AtualizarTipo(string Tipo) => this.Tipo = Tipo;
        public void AtualizarAbertura(DateTime? Abertura) => this.Abertura = Abertura;
        public void AtualizarLogradouro(string Logradouro) => this.Logradouro = Logradouro;
        public void AtualizarNumero(string Numero) => this.Numero = Numero;
        public void AtualizarComplemento(string Complemento) => this.Complemento = Complemento;
        public void AtualizarCep(string Cep) => this.Cep = Cep;
        public void AtualizarMunicipio(string Municipio) => this.Municipio = Municipio;
        public void AtualizarUF(string UF) => this.UF = UF;
        public void AtualizarEmail(string Email) => this.Email = Email;
        public void AtualizarTelefone(string Telefone) => this.Telefone = Telefone;
        public void AtualizarCnpjUnico(Int64? CnpjUnico) => this.CnpjUnico = CnpjUnico;
        #endregion
    }
}
