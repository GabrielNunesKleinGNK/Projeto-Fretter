namespace Fretter.Domain.Entities
{
    public class OcorrenciaTipo : EntityBase
    {
        #region "Construtores"
        public OcorrenciaTipo(int Id, string Nome)
        {
            this.Ativar();
            this.Id = Id;
            this.Nome = Nome;
            this.Ativo = Ativo;
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
