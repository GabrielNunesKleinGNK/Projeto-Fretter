using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class Grupo : EntityBase
    {
        #region "Construtores"
        public Grupo(int id, int empresaId, string codigo, string descricao)
        {
            this.Ativar();
            this.Id = id;
            this.EmpresaId = empresaId;
            this.Codigo = codigo;
            this.Descricao = descricao;
        }
        #endregion

        #region "Propriedades"
        public int EmpresaId { get; protected set; }
        public string Codigo { get; protected set; }
        public string Descricao { get; protected set; }
        #endregion

        #region "Referencias"
        public Empresa Empresa { get; protected set; }
        #endregion

        #region "Métodos"
        #endregion
    }
}
