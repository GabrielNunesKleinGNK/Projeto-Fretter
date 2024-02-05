using System;

namespace Fretter.Domain.Entities
{
    public class ImportacaoCteImposto : EntityBase
    {
        #region "Construtores"
        public ImportacaoCteImposto(int Id, int ImportacaoCteId, string Classificacao)
        {
            this.Ativar();
            this.Id = Id;
            this.ImportacaoCteId = ImportacaoCteId;
            this.Classificacao = Classificacao;
        }
        #endregion

        #region "Propriedades"
        public int ImportacaoCteId { get; protected set; }
        public string Classificacao { get; protected set; }
        public Decimal? ValorBaseCalculo { get; protected set; }
        public Decimal? Aliquota { get; protected set; }
        public Decimal? Valor { get; protected set; }
        #endregion

        #region "Referencias"
        public ImportacaoCte ImportacaoCte { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarImportacaoCteId(int ImportacaoCteId) => this.ImportacaoCteId = ImportacaoCteId;
        public void AtualizarValorBaseCalculo(Decimal? ValorBaseCalculo) => this.ValorBaseCalculo = ValorBaseCalculo;
        public void AtualizarValor(Decimal? Valor) => this.Valor = Valor;
        public void AtualizarAliquota(Decimal? Aliquota) => this.Aliquota = Aliquota;

        #endregion
    }
}
