using Fretter.Domain.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoCEPCompletoModel
    {
        public int Cd_Id { get; set; }
        public int Id_Regiao { get; set; }
        [Display(Name = "Início")]
        public string Cd_CepInicio { get; set; }
        [Display(Name = "Fim")]
        public string Cd_CepFim { get; set; }
        [Display(Name = "Prazo")]
        public byte? Nr_Prazo { get; set; }
        [Display(Name = "GRIS")]
        public decimal? Nr_PercentualGRIS { get; set; }
        [Display(Name = "Frete Adicional")]
        public decimal? Nr_FreteAdicional { get; set; }
        [DataTableColumnIgnore]
        private int cepInicio;
        [DataTableColumnIgnore]
        internal int CepInicio => cepInicio == default ? (cepInicio = int.Parse(Cd_CepInicio)) : cepInicio;
        [DataTableColumnIgnore]
        internal int cepFim;
        [DataTableColumnIgnore]
        internal int CepFim => cepFim == default ? (cepFim = int.Parse(Cd_CepFim)) : cepFim;

        [Display(Name = "GRIS Minímo (R$)")]
        public decimal Nr_ValorMinimoGRIS { get; set; }


        [Display(Name = "AdValorem Minímo (R$)")]
        public decimal Nr_ValorMinimoAdvalorem { get; set; }


        [Display(Name = "Fluvial Minímo(R$)")]
        public decimal Nr_ValorMinimoFluvial { get; set; }


        [Display(Name = "SUFRAMA (R$)")]
        public decimal Nr_ValorSuframa { get; set; }

        [Display(Name = "TRT Minímo (R$)")]
        public decimal Nr_ValorMinimoTRT { get; set; }


        [Display(Name = "TRT (%)")]
        public decimal Nr_PercentualTRT { get; set; }

        [Display(Name = "TDE Minímo (R$)")]
        public decimal Nr_ValorMinimoTDE { get; set; }

        [Display(Name = "TDE (%)")]
        public decimal Nr_PercentualTDE { get; set; }

        [Display(Name = "TDA Minímo (R$)")]
        public decimal Nr_ValorMinimoTDA { get; set; }
        [Display(Name = "TDA (%)")]
        public decimal Nr_PercentualTDA { get; set; }

        [Display(Name = "CT-E (R$)")]
        public decimal Nr_ValorCTE { get; set; }
        [Display(Name = "Taxa de Risco (R$)")]

        public decimal Nr_ValorTaxaRisco { get; set; }

        public bool Flg_ZonaDificuldade { get; set; }
    }
}
