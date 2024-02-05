using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuEstoque: EntityBase
    {
        #region "Construtores"
        public SkuEstoque(int id, string cdSku, string cnpj, decimal quantidade, decimal qtdCrossdoking, int prazoExpedicao, int prazoCrossdoking)
        {
            this.Ativar();
            this.Id = id;
            this.CdSku = cdSku;
            this.Cnpj = cnpj;
            this.Quantidade = quantidade;
            this.QtdCrossdoking = qtdCrossdoking;
            this.PrazoExpedicao = prazoExpedicao;
            this.PrazoCrossdoking = prazoCrossdoking;
        }
        #endregion

        #region "Propriedades"
        [Required]
        public string CdSku { get; protected set; }
        [Required]
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
