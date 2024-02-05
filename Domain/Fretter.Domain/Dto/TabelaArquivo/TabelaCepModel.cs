namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class TabelaCepModel
    {
        public int Cd_Id { get; set; }
        public byte? Nr_Prazo { get; set; }
        public decimal? Nr_PercentualGRIS { get; set; }
        public decimal? Nr_FreteAdicional { get; set; }
        public bool Flg_ZonaDificuldade { get; set; }
    }
}
