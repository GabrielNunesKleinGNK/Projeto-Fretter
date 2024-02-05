using System;

namespace Fretter.Domain.Entities
{
    public class EmpresaImportacaoDetalhe : EntityBase
    {
        #region "Construtores"
        public EmpresaImportacaoDetalhe(int Id)
        {
            this.Ativar();
            this.Id = Id;
        }
        public EmpresaImportacaoDetalhe(int Id, int EmpresaImportacaoArquivoId, int? EmpresaId, string Token, string Email, string Nome, string Cnpj, string CEP, string UF, bool CorreioBalcao, bool ConsomeApiFrete)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaImportacaoArquivoId = EmpresaImportacaoArquivoId;
            this.EmpresaId = EmpresaId;
            this.Token = Token;
            this.Email = Email;
            this.Nome = Nome;
            this.Cnpj = Cnpj;
            this.CEP = CEP;
            this.UF = UF;
            this.CorreioBalcao = CorreioBalcao;
            this.ConsomeApiFrete = ConsomeApiFrete;
        }
        #endregion

        #region "Propriedades"                
        public int EmpresaImportacaoArquivoId { get; protected set; }
        public int? EmpresaId { get; protected set; }
        public string Token { get; protected set; }
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Cnpj { get; protected set; }
        public string CEP { get; protected set; }
        public string UF { get; protected set; }
        public string Descricao { get; protected set; }
        public bool CorreioBalcao { get; protected set; }
        public bool ConsomeApiFrete { get; protected set; }
        public bool SucessoEmpresa { get; protected set; }
        public bool SucessoCanal { get; protected set; }
        public bool SucessoPermissao { get; protected set; }
        #endregion

        #region "Referencias"
        public EmpresaImportacaoArquivo EmpresaImportacaoArquivo { get; protected set; }
        public Empresa Empresa { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarToken(string token) => this.Token = token;
        public void AtualizarEmail(string email) => this.Email = email;
        public void AtualizarCnpj(string Cnpj) => this.Cnpj = Cnpj;
        public void AtualizarCEP(string CEP) => this.CEP = CEP;
        public void AtualizarUF(string UF) => this.UF = UF;
        public void AtualizarEmpresa(int empresaId) => this.EmpresaId = empresaId;
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        public void AtualizarSucessoEmpresa(bool sucesso) => this.SucessoEmpresa = sucesso;
        public void AtualizarSucessoCanal(bool sucesso) => this.SucessoCanal = sucesso;
        public void AtualizarSucessoPermissao(bool sucesso) => this.SucessoPermissao = sucesso;
        #endregion
    }
}


