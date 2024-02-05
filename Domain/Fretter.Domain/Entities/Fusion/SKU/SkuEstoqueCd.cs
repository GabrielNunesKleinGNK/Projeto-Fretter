using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuEstoqueCd: EntityBase
    {
        #region "Construtores"
        public SkuEstoqueCd(string cnpj, decimal quantidade)
        {
            this.Cnpj = cnpj;
            this.Quantidade = quantidade;
        }

        public SkuEstoqueCd(int id, string cnpj, decimal quantidade, decimal qtdCrossdoking, int prazoExpedicao, int prazoCrossdoking)
        {
            this.Ativar();
            this.Id = id;
            this.Cnpj = cnpj;
            this.Quantidade = quantidade;
            this.QtdCrossdoking = qtdCrossdoking;
            this.PrazoExpedicao = prazoExpedicao;
            this.PrazoCrossdoking = prazoCrossdoking;
        }
        #endregion

        #region "Propriedades"
        public string Cnpj { get; protected set; }
        public decimal Quantidade { get; protected set; }
        public decimal QtdCrossdoking { get; protected set; }
        public int PrazoExpedicao { get; protected set; }
        public int PrazoCrossdoking { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        #endregion
    }
}
