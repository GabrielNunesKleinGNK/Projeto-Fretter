using System;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucaoLog : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucaoLog()
        {
            this.Ativar();
        }
        public EntregaDevolucaoLog(int Id, int EntregaDevolucaoTipoId, int EntregaDevolucaoId, string JsonEnvio, string JsonRetorno, string Observacao, bool Sucesso)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaDevolucaoTipoId = EntregaDevolucaoTipoId;
            this.EntregaDevolucaoId = EntregaDevolucaoId;
            this.JsonEnvio = JsonEnvio;
            this.JsonRetorno = JsonRetorno;
            this.Observacao = Observacao;
            this.Sucesso = Sucesso;

        }
        #endregion

        #region "Propriedades"
        public int EntregaDevolucaoTipoId { get; protected set; }
        public int EntregaDevolucaoId { get; protected set; }
        public string JsonEnvio { get; protected set; }
        public string JsonRetorno { get; protected set; }
        public string Observacao { get; protected set; }
        public bool? Sucesso { get; protected set; }
        #endregion

        #region "Referencias" 
        #endregion

        #region "Métodos"
        public void AtualizarEntregaDevolucaoTipoId(int EntregaDevolucaoTipoId) => this.EntregaDevolucaoTipoId = EntregaDevolucaoTipoId;
        public void AtualizarEntregaDevolucaoId(int EntregaDevolucaoId) => this.EntregaDevolucaoId = EntregaDevolucaoId;
        public void AtualizarSucesso(bool Sucesso) => this.Sucesso = Sucesso;
        #endregion
    }
}
