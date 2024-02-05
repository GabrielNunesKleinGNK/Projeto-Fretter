using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class MenuFreteRegiaoCepCapacidade : EntityBase
    {
        #region Construtores
        private MenuFreteRegiaoCepCapacidade()
        {

        }
        public MenuFreteRegiaoCepCapacidade(int id, int idRegiaoCEP, int idPeriodo, byte nrDia, int vlQuantidade, int vlQuantidadeDisponivel, decimal nrValor)
        {
            this.Id = id;
            this.IdRegiaoCEP = idRegiaoCEP;
            this.IdPeriodo = idPeriodo;
            this.NrDia = nrDia;
            this.VlQuantidade = vlQuantidade;
            this.VlQuantidadeDisponivel = vlQuantidadeDisponivel;
            this.NrValor = nrValor;
        }
        #endregion

        #region "Propriedades"
        public int IdRegiaoCEP { get; protected set; }
        public int IdPeriodo { get; protected set; }
        public byte NrDia { get; protected set; }
        public int VlQuantidade { get; protected set; }
        public int VlQuantidadeDisponivel { get; protected set; }
        public decimal NrValor { get; protected set; }
        #endregion
    }
}
