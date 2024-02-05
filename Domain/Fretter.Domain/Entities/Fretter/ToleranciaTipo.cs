using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class ToleranciaTipo : EntityBase
    {
		#region "Construtores"
        private ToleranciaTipo() { }
        public ToleranciaTipo(int id, string descricao)
		{
			this.AtualizarId(id);
            AtualizarDescricao(descricao);
		}

        #endregion

        #region "Propriedades"
        public string Descricao { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        #endregion
    }
}      
