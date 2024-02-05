using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaOrigemImportacao : EntityBase
    {
        #region "Construtores"
        public EntregaOrigemImportacao(int Id, string Nome)
        {
            this.Id = Id;
            this.Nome = Nome;
        }
        #endregion

        #region "Propriedades"  

        public string Nome { get; protected set; }

        #endregion

        #region "Referencias"

        #endregion

        #region "Métodos"
 
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        #endregion
    }
}

