using System;

namespace Fretter.Domain.Entities
{
    public class ImportacaoCteCarga : EntityBase
    {
        #region "Construtores"
        private ImportacaoCteCarga() { }
        public ImportacaoCteCarga(int id, int importacaoCteId, string tipo, string codigo, decimal? quantidade, int? configuracaoCteTransportadorId)
        {
            this.Ativar();
            this.AtualizarId(id);
            this.AtualizarImportacaoCteId(importacaoCteId);
            this.AtualizarTipo(tipo);
            this.AtualizarCodigo(codigo);
            this.AtualizarQuantidade(quantidade > 999999 ? 0 : quantidade);
            this.AtualizarConfiguracaoCteTipoId(configuracaoCteTransportadorId);
        }
        #endregion

        #region "Propriedades"
        public int ImportacaoCteId { get; protected set; }
        public string Tipo { get; protected set; }
        public string Codigo { get; protected set; }
        public decimal? Quantidade { get; protected set; }
        public int? ConfiguracaoCteTipoId { get; protected set; }
        #endregion

        #region "Referencias"
        public ImportacaoCte ImportacaoCte { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarImportacaoCteId(int ImportacaoCteId) => this.ImportacaoCteId = ImportacaoCteId;
        public void AtualizarTipo(string Tipo) => this.Tipo = Tipo;
        public void AtualizarCodigo(string Codigo) => this.Codigo = Codigo;
        public void AtualizarQuantidade(decimal? Quantidade) => this.Quantidade = Quantidade;

        public void AtualizarConfiguracaoCteTipoId(int? configCteTransportadorId) => this.ConfiguracaoCteTipoId = configCteTransportadorId;
        #endregion
    }
}
