using System;

namespace Fretter.Domain.Entities
{
    public class TabelaTipoCanalVenda : EntityBase
    {
        #region "Construtores"
        public TabelaTipoCanalVenda(int Id, bool PriorizaPrazo, bool AceitaPrazoZerado, int CanalVendaId)
        {
            this.Ativar();
            this.Id = Id;
            this.PriorizaPrazo = PriorizaPrazo;            
            this.AceitaPrazoZerado = AceitaPrazoZerado;
            this.CanalVendaId = CanalVendaId;
        }
        #endregion

        #region "Propriedades"        
        public int CanalVendaId { get; protected set; }
        public bool PriorizaPrazo { get; protected set; }
        public string Alias { get; protected set; }
        public bool AceitaPrazoZerado { get; protected set; }
        #endregion

        #region "Referencias"
        public CanalVenda CanalVenda { get; protected set; }        
        #endregion

        #region "Métodos"
        public void AtualizarPriorizaPrazo(bool PriorizaPrazo) => this.PriorizaPrazo = PriorizaPrazo;
        public void AtualizarAlias(string Alias) => this.Alias = Alias;
        public void AtualizarAceitaPrazoZerado(bool AceitaPrazoZerado) => this.AceitaPrazoZerado = AceitaPrazoZerado;
        public void AtualizaCanalVenda(bool AceitaPrazoZerado) => this.AceitaPrazoZerado = AceitaPrazoZerado;
        #endregion
    }
}

