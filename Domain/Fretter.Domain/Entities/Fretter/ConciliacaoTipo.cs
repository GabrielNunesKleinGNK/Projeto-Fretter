using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ConciliacaoTipo : EntityBase
    {
        #region "Construtores"
        public ConciliacaoTipo(int Id, string Nome)
        {
            this.Ativar();
            this.Id = Id;
            this.Nome = Nome;
        }
        #endregion

        #region "Propriedades"
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual Conciliacao Conciliacao { get; set; }
        public virtual IEnumerable<ImportacaoArquivoTipoItem> ImportacaoArquivoTipoItems { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
        #endregion
    }
}
