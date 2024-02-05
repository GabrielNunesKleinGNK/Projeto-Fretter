using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaTransporteTipoItem : EntityBase
    {
        #region "Construtores"
        public EmpresaTransporteTipoItem(int Id, int EmpresaTransporteTipoId, int TransportadorId, string Url, string Alias, string CodigoIntegracao)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaTransporteTipoId = EmpresaTransporteTipoId;
            this.TransportadorId = TransportadorId;
            this.Url = Url;
            this.Alias = Alias;
            this.CodigoIntegracao = CodigoIntegracao;
        }
        #endregion

        #region "Propriedades"
        public int EmpresaTransporteTipoId { get; protected set; }
        public int TransportadorId { get; protected set; }
        public string Url { get; protected set; }
        public string Alias { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        #endregion                 

        #region "Referencias"
        public virtual Transportador Transportador { get; set; }
        public virtual EmpresaTransporteTipo EmpresaTransporteTipo { get; set; }
        public virtual List<EmpresaTransporteConfiguracao> EmpresaTransporteConfiguracoes { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaTransporteTipoId(int EmpresaTransporteTipoId) => this.EmpresaTransporteTipoId = EmpresaTransporteTipoId;
        public void AtualizarTransportadorId(int TransportadorId) => this.TransportadorId = TransportadorId;
        public void AtualizarUrl(string Url) => this.Url = Url;
        public void AtualizarAlias(string Alias) => this.Alias = Alias;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AdicionarConfiguracao(EmpresaTransporteConfiguracao configuracao)
        {
            if (this.EmpresaTransporteConfiguracoes == null)
                this.EmpresaTransporteConfiguracoes = new List<EmpresaTransporteConfiguracao>();

            this.EmpresaTransporteConfiguracoes.Add(configuracao);
        }
        #endregion
    }
}
