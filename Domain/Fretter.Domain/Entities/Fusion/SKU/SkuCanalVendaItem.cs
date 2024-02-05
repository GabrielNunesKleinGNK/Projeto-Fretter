using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuCanalVendaItem: EntityBase
    {
        #region "Construtores"
        public SkuCanalVendaItem(int id, string cdSku, string canalVenda, decimal preco, decimal precoPromocional)
        {
            this.Ativar();
            this.Id = id;
            this.CanalVenda = canalVenda;
            this.Preco = preco;
            this.PrecoPromocional = precoPromocional;
        }
        #endregion

        #region "Propriedades"
        [Required]
        public string CdSku { get; protected set; }
        [Required]
        public string CanalVenda { get; protected set; }
        public decimal Preco { get; protected set; }
        public decimal PrecoPromocional { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        #endregion
    }
}
