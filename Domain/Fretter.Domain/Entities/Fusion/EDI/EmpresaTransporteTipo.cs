using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaTransporteTipo : EntityBase
    {
        #region "Construtores"
        public EmpresaTransporteTipo(int Id, string Nome)
        {
            this.Ativar();
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
