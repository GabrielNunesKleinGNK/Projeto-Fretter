using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucaoStatus : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucaoStatus(int Id, int EntregaTransporteTipoId, string Nome)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaTransporteTipoId = EntregaTransporteTipoId;
            this.Nome = Nome;
        }
        #endregion

        #region "Propriedades"
        public int EntregaTransporteTipoId { get; protected set; }
        public string Nome { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string Alias { get; protected set; }
        public bool? MonitoraOcorrencia { get; protected set; }
        #endregion

        #region "Referencias"
        public List<EntregaDevolucao> EntregasDevolucoes { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEntregaTransporteTipoId(int EntregaTransporteTipoId) => this.EntregaTransporteTipoId = EntregaTransporteTipoId;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarAlias(string Alias) => this.Alias = Alias;
        #endregion
    }
}
