using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucaoStatusAcao : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucaoStatusAcao(int Id, int EntregaTransporteTipoId, int EntregaDevolucaoStatusId, int EntregaDevolucaoAcaoId, int EntregaDevolucaoResultadoId, bool? InformaMotivo, bool? Visivel)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaTransporteTipoId = EntregaTransporteTipoId;
            this.EntregaDevolucaoStatusId = EntregaDevolucaoStatusId;
            this.EntregaDevolucaoAcaoId = EntregaDevolucaoAcaoId;
            this.EntregaDevolucaoResultadoId = EntregaDevolucaoResultadoId;
            this.InformaMotivo = InformaMotivo;
            this.Visivel = Visivel;
        }
        #endregion

        #region "Propriedades"
        public int EntregaTransporteTipoId { get; protected set; }
        public int EntregaDevolucaoStatusId { get; protected set; }
        public int EntregaDevolucaoAcaoId { get; protected set; }
        public int EntregaDevolucaoResultadoId { get; protected set; }
        public bool? Visivel { get; protected set; }
        public bool? InformaMotivo { get; protected set; }
        #endregion

        #region "Referencias"
        public EntregaDevolucaoAcao Acao { get; set; }

        #endregion

        #region "Métodos"
        public void AtualizarEntregaTransporteTipoId(int EntregaTransporteTipoId) => this.EntregaTransporteTipoId = EntregaTransporteTipoId;
        public void AtualizarEntregaDevolucaoStatusId(int EntregaDevolucaoStatusId) => this.EntregaDevolucaoStatusId = EntregaDevolucaoStatusId;
        public void AtualizarEntregaDevolucaoAcaoId(int EntregaDevolucaoAcaoId) => this.EntregaDevolucaoAcaoId = EntregaDevolucaoAcaoId;
        public void AtualizarEntregaDevolucaoResultadoId(int EntregaDevolucaoResultadoId) => this.EntregaDevolucaoResultadoId = EntregaDevolucaoResultadoId;
        public void AtualizarInformaMotivo(bool? InformaMotivo) => this.InformaMotivo = InformaMotivo;
        #endregion
    }
}
