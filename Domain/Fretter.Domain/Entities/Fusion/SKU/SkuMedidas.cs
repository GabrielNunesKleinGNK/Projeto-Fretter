using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuMedidas: EntityBase
    {
        #region "Construtores"
        public SkuMedidas(int id, decimal largura, decimal altura, decimal comprimento, int divisor = 1)
        {
            this.Ativar();
            this.Id = id;
            this.Largura = largura/divisor;
            this.Altura = altura/divisor;
            this.Comprimento = comprimento/divisor;
        }
        #endregion

        #region "Propriedades"
        [Required]
        public decimal Largura { get; protected set; }
        [Required]
        public decimal Altura { get; protected set; }
        [Required]
        public decimal Comprimento { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        #endregion
    }
}
