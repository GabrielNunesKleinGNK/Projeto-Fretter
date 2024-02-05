using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class Tipo : EntityBase
    {
        #region "Construtores"       
        public Tipo(int Id, string Descricao)
        {
            this.Ativar();
            this.Id = Id;
            this.Descricao = Descricao;     
        }
        #endregion

        #region "Propriedades"		
        public string Descricao { get; set; }
        #endregion

        #region "Referencias"
    
        #endregion

        #region "Métodos"
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        #endregion
    }
}
