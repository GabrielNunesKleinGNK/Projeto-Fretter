using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaStatusAcao : EntityBase
    {
        #region "Construtores"
        public FaturaStatusAcao(int Id, int FaturaStatusId, int FaturaAcaoId, int FaturaStatusResultadoId, bool Visivel, bool InformaMotivo)
        {
            this.Ativar();
            this.Id = Id;
            this.FaturaStatusId = FaturaStatusId;
            this.FaturaAcaoId = FaturaAcaoId;
            this.FaturaStatusResultadoId = FaturaStatusResultadoId;
            this.Visivel = Visivel;
            this.InformaMotivo = InformaMotivo;
        }
        #endregion

        #region "Propriedades"
        public int FaturaStatusId { get; protected set; }
        public int FaturaAcaoId { get; protected set; }
        public int FaturaStatusResultadoId { get; protected set; }
        public bool Visivel { get; protected set; }
        public bool InformaMotivo { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual FaturaAcao FaturaAcao { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarFaturaStatusId(int FaturaStatusId) => this.FaturaStatusId = FaturaStatusId;
        public void AtualizarFaturaAcaoId(int FaturaAcaoId) => this.FaturaAcaoId = FaturaAcaoId;
        public void AtualizarFaturaStatusResultadoId(int FaturaStatusResultadoId) => this.FaturaStatusResultadoId = FaturaStatusResultadoId;
        public void AtualizarVisivel(bool Visivel) => this.Visivel = Visivel;
        public void AtualizarInformaMotivo(bool InformaMotivo) => this.InformaMotivo = InformaMotivo;
        #endregion
    }
}
