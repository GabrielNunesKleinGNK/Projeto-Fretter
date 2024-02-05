using System;

namespace Fretter.Domain.Entities
{
    public class ImportacaoCteComposicao : EntityBase
    {
        #region "Construtores"
        protected ImportacaoCteComposicao() { }
        public ImportacaoCteComposicao(int id, int importacaoCteId, string nome, decimal? valor, int? configCteTipoId)
        {
            this.Ativar();
            this.Id = id;
            this.ImportacaoCteId = importacaoCteId;
            this.Nome = nome;
            this.Valor = ((decimal)valor) > 9999999 ? 0 : ((decimal)valor);
            this.AtualizarConfiguracaoCteTipoId(configCteTipoId);
        }
        #endregion

        #region "Propriedades"
        public int ImportacaoCteId { get; protected set; }
        public string Nome { get; protected set; }
        public decimal Valor { get; protected set; }
        public int? ConfiguracaoCteTipoId { get; protected set; }
        #endregion

        #region "Referencias"
        public ImportacaoCte ImportacaoCte { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarImportacaoCteId(int ImportacaoCteId) => this.ImportacaoCteId = ImportacaoCteId;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarValor(decimal Valor) => this.Valor = Valor;

        public void AtualizarConfiguracaoCteTipoId(int? configCteTipoId) => this.ConfiguracaoCteTipoId = configCteTipoId;
        #endregion
    }
}
