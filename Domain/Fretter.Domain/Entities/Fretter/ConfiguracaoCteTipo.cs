using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class ConfiguracaoCteTipo : EntityBase
    {
		#region "Construtores"
        private ConfiguracaoCteTipo() { }
        public ConfiguracaoCteTipo(int id, string descricao, string chave)
		{
			this.AtualizarId(id);
            AtualizarDescricao(descricao);
            AtualizarChave(chave);
        }

        #endregion

        #region "Propriedades"
        public string Descricao { get; protected set; }
        public string Chave { get; set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        public void AtualizarChave(string chave) => this.Chave = chave;
        #endregion
    }
}      
