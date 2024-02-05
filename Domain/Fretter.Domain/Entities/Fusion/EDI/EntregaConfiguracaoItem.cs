using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaConfiguracaoItem : EntityBase
    {
        #region "Construtores"
        public EntregaConfiguracaoItem(int Id, int EntregaConfiguracaoId, int EntregaConfiguracaoItemTipoId, string Url, 
            string Verbo, string Layout, string LayoutHeader, DateTime? DataProcessamento, bool? ProcessadoSucesso)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaConfiguracaoId = EntregaConfiguracaoId;
            this.EntregaConfiguracaoItemTipoId = EntregaConfiguracaoItemTipoId;
            this.Url = Url;
            this.Verbo = Verbo;
            this.Layout = Layout;
            this.LayoutHeader = LayoutHeader;
            this.DataProcessamento = DataProcessamento;
            this.ProcessadoSucesso = ProcessadoSucesso;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public int EntregaConfiguracaoId { get; protected set; }
        public int EntregaConfiguracaoItemTipoId { get; protected set; }
        public string Url { get; protected set; }
        public string Verbo { get; protected set; }
        public string Layout { get; protected set; }
        public string LayoutHeader { get; protected set; }
        public DateTime? DataProcessamento { get; protected set; }
        public bool? ProcessadoSucesso { get; protected set; }
        #endregion

        #region "Referencias"
        //public virtual EntregaConfiguracao EntregaConfiguracao { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEntregaConfiguracaoId(int EntregaConfiguracaoId) => this.EntregaConfiguracaoId = EntregaConfiguracaoId;
        public void AtualizarEntregaConfiguracaoItemTipoId(int EntregaConfiguracaoItemTipoId) => this.EntregaConfiguracaoItemTipoId = EntregaConfiguracaoItemTipoId;
        public void AtualizarUrl(string URL) => this.Url = URL;
        public void AtualizarVerbo(string Verbo) => this.Verbo = Verbo;
        public void AtualizarLayout(string Layout) => this.Layout = Layout;
        public void AtualizarLayoutHeader(string LayoutHeader) => this.LayoutHeader = LayoutHeader;
        public void AtualizarProcessamentoSucesso(bool? ProcessadoSucesso) => this.ProcessadoSucesso = ProcessadoSucesso;
        public void AtualizarProcessamento(DateTime? Processamento) => this.DataProcessamento = Processamento;
        #endregion
    }
}
