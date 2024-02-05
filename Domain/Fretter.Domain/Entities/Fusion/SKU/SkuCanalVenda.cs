using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuCanalVenda: EntityBase
    {
        #region "Construtores"
        public SkuCanalVenda()
        {}

        public SkuCanalVenda(int id, string canalVenda, decimal preco, decimal precoPromocional)
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
        public string CanalVenda { get; protected set; }
        public decimal Preco { get; protected set; }
        public decimal PrecoPromocional { get; protected set; }

        #endregion

        #region "Referencias"
        public SkuCategoria[] Categorias { get; set; }
        #endregion

        #region "Métodos"
        #endregion
    }
}
