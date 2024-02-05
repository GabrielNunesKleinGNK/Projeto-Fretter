using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class SkuCategoria: EntityBase
    {
        #region "Construtores"
        public SkuCategoria(int id, string cdCategoria, string Nome, string cdCategoriaPai)
        {
            this.Ativar();
            this.Id = id;
            this.CdCategoria = cdCategoria;
            this.Nome = Nome;
            this.CdCategoriaPai = cdCategoriaPai;
        }
        #endregion

        #region "Propriedades"
        public string CdCategoria { get; protected set; }
        public string Nome { get; protected set; }
        public string CdCategoriaPai { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        #endregion
    }
}
