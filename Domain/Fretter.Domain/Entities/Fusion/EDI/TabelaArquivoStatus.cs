using System;

namespace Fretter.Domain.Entities
{
    public class TabelaArquivoStatus : EntityBase
    {
        #region "Construtores"
        public TabelaArquivoStatus(int Id, string Status, DateTime Criacao)
        {
            this.Ativar();
            this.Id = Id;
            this.Status = Status;
            this.Criacao = Criacao;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public string Status { get; protected set; }
        public DateTime Criacao { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarStatus(string status) => this.Status = status;
        public void AtualizarInclusao(DateTime criacao) => this.Criacao = criacao;
        #endregion
    }
}
