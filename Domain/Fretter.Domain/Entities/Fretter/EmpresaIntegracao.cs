using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaIntegracao : EntityBase
    {
        #region "Construtores"
        public EmpresaIntegracao(int Id, int? EmpresaId, int? CanalVendaId, string URLBase, string URLToken, string ApiKey, string Usuario, string Senha,
            string ClientId, string ClientSecret, string ClientScope, int? EntregaOrigemImportacaoId, bool Ativo)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.CanalVendaId = CanalVendaId;
            this.EmpresaId = EmpresaId;
            this.URLBase = URLBase;
            this.URLToken = URLToken;
            this.ApiKey = ApiKey;
            this.Usuario = Usuario;
            this.Senha = Senha;
            this.ClientId = ClientId;
            this.ClientSecret = ClientSecret;
            this.ClientScope = ClientScope;
            this.EntregaOrigemImportacaoId = EntregaOrigemImportacaoId;
            this.Ativo = Ativo;
        }

        public EmpresaIntegracao(int Id, int? EmpresaId, int? CanalVendaId, string URLBase, string URLToken, string ApiKey, string Usuario, string Senha,
            string ClientId, string ClientSecret, string ClientScope, int? EntregaOrigemImportacaoId, List<EmpresaIntegracaoItem> integracoes, bool Ativo)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.CanalVendaId = CanalVendaId;
            this.EmpresaId = EmpresaId;
            this.URLBase = URLBase;
            this.URLToken = URLToken;
            this.ApiKey = ApiKey;
            this.Usuario = Usuario;
            this.Senha = Senha;
            this.ClientId = ClientId;
            this.ClientSecret = ClientSecret;
            this.ClientScope = ClientScope;
            this.EntregaOrigemImportacaoId = EntregaOrigemImportacaoId;
            this.ListaIntegracoes = new List<EmpresaIntegracaoItem>();
            this.ListaIntegracoes.AddRange(integracoes);
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"  

        public int? EmpresaId { get; protected set; }
        public int? CanalVendaId { get; protected set; }
        public string URLBase { get; protected set; }
        public string URLToken { get; protected set; }
        public string ApiKey { get; protected set; }
        public string Usuario { get; protected set; }
        public string Senha { get; protected set; }
        public string ClientId { get; protected set; }
        public string ClientSecret { get; protected set; }
        public string ClientScope { get; protected set; }
        public int? EntregaOrigemImportacaoId { get; protected set; }

        #endregion

        #region "Referencias"
        public List<EmpresaIntegracaoItem> ListaIntegracoes { get; set; }

        #endregion

        #region "Métodos"
        public void AtualizarEmpresaId(int? EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarCanalVendaId(int? CanalVendaId) => this.CanalVendaId = CanalVendaId;
        public void AtualizarURLBase(string URLBase) => this.URLBase = URLBase;
        public void AtualizarURLToken(string URLToken) => this.URLToken = URLToken;
        public void AtualizarApiKey(string ApiKey) => this.ApiKey = ApiKey;
        public void AtualizarUsuario(string Usuario) => this.Usuario = Usuario;
        public void AtualizarSenha(string Senha) => this.Senha = Senha;
        public void AtualizarClientId(string ClientId) => this.ClientId = ClientId;
        public void AtualizarClientSecret(string ClientSecret) => this.ClientSecret = ClientSecret;
        public void AtualizarClientScope(string ClientScope) => this.ClientScope = ClientScope;
        public void AtualizarEntregaOrigemImportacaoId(int? EntregaOrigemImportacaoId) => this.EntregaOrigemImportacaoId = EntregaOrigemImportacaoId;
        #endregion
    }
}

