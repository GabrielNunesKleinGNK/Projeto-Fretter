using System;

namespace Fretter.Domain.Entities
{
    public class ImportacaoCteNotaFiscal : EntityBase
    {
        #region "Construtores"
        public ImportacaoCteNotaFiscal(int Id, int ImportacaoCteId, string Chave, DateTime? DataPrevista)
        {
            this.Ativar();
            this.Id = Id;
            this.ImportacaoCteId = ImportacaoCteId;
            this.Chave = Chave;
            this.DataPrevista = DataPrevista;
        }

        public ImportacaoCteNotaFiscal(int Id, int ImportacaoCteId, string Chave, DateTime? DataPrevista, string CNPJEmissorNF, string NumeroNF, string SerieNF, DateTime? DataEmissaoNF)
        {
            this.Ativar();
            this.Id = Id;
            this.ImportacaoCteId = ImportacaoCteId;
            this.Chave = Chave;
            this.DataPrevista = DataPrevista;
            this.CNPJEmissorNF = CNPJEmissorNF;
            this.NumeroNF = NumeroNF;
            this.SerieNF = SerieNF;
            this.DataEmissaoNF = DataEmissaoNF;
        }
        #endregion

        #region "Propriedades"
        public int ImportacaoCteId { get; protected set; }
        public string Chave { get; protected set; }
        public DateTime? DataPrevista { get; protected set; }
        public string CNPJEmissorNF { get; protected set; }
        public string NumeroNF { get; protected set; }
        public string SerieNF { get; protected set; }
        public DateTime? DataEmissaoNF { get; protected set; }

        #endregion

        #region "Referencias"
        public ImportacaoCte ImportacaoCte { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarImportacaoCteId(int ImportacaoCteId) => this.ImportacaoCteId = ImportacaoCteId;
        public void AtualizarChave(string Chave) => this.Chave = Chave;
        public void AtualizarDataPrevista(DateTime? DataPrevista) => this.DataPrevista = DataPrevista;
        #endregion
    }
}
