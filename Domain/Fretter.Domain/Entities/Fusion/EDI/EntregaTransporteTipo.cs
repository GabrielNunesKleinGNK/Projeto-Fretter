using System;

namespace Fretter.Domain.Entities
{
    public class EntregaTransporteTipo : EntityBase
    {
        #region "Construtores"
        public EntregaTransporteTipo(int Id, int EntregaTransporteServicoId, string Nome, string URL, string Layout, string ApiKey, string Usuario, string Senha, Int16? DiasValidadeMinimo, Int16? DiasValidadeMaximo, string Alias, string CodigoIntegracao, bool? Producao)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaTransporteServicoId = EntregaTransporteServicoId;
            this.Nome = Nome;
            this.URL = URL;
            this.Layout = Layout;
            this.ApiKey = ApiKey;
            this.Usuario = Usuario;
            this.Senha = Senha;
            this.DiasValidadeMinimo = DiasValidadeMinimo;
            this.DiasValidadeMaximo = DiasValidadeMaximo;
            this.Alias = Alias;
            this.CodigoIntegracao = CodigoIntegracao;
            this.Producao = Producao;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public int EntregaTransporteServicoId { get; protected set; }
        public string Nome { get; protected set; }
        public string URL { get; protected set; }
        public string Layout { get; protected set; }
        public string ApiKey { get; protected set; }
        public string Usuario { get; protected set; }
        public string Senha { get; protected set; }
        public Int16? DiasValidadeMinimo { get; protected set; }
        public Int16? DiasValidadeMaximo { get; protected set; }
        public string Alias { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public bool? Producao { get; protected set; }
        #endregion

        #region "Referencias"
        public EntregaTransporteServico EntregaTransporteServico { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEntregaTransporteServico(int EntregaTransporteServico) => this.EntregaTransporteServicoId = EntregaTransporteServico;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarURL(string URL) => this.URL = URL;
        public void AtualizarLayout(string Layout) => this.Layout = Layout;
        public void AtualizarApiKey(string ApiKey) => this.ApiKey = ApiKey;
        public void AtualizarUsuario(string Usuario) => this.Usuario = Usuario;
        public void AtualizarSenha(string Senha) => this.Senha = Senha;
        public void AtualizarDiasValidadeMinimo(Int16? DiasValidadeMinimo) => this.DiasValidadeMinimo = DiasValidadeMinimo;
        public void AtualizarDiasValidadeMaximo(Int16? DiasValidadeMaximo) => this.DiasValidadeMaximo = DiasValidadeMaximo;
        public void AtualizarAlias(string Alias) => this.Alias = Alias;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarProducao(bool? Producao) => this.Producao = Producao;
        #endregion
    }
}
