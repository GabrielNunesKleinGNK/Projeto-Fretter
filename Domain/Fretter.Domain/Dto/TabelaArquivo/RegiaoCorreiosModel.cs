using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoCorreiosModel
    {
        [Display(Name = "Código", ShortName = "Cod.")]
        public int Cd_Id { get; set; }


        [Display(Name = "Nome", ShortName = "Nome")]
        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Text)]
        public string Ds_Regiao { get; set; }

        [Display(Name = "Peso Excedente")]
        public decimal Nr_PesoExcedente { get; set; }

        [Display(Name = "Pedágio Fixo")]
        public decimal Nr_PedagioFixo { get; set; }

        [Display(Name = "Pedágio Variável")]
        public decimal Nr_PedagioVariavel { get; set; }

        [Display(Name = "Fração Peso Pedágio")]
        public decimal Nr_FracaoPesoPedagio { get; set; }

        [Display(Name = "GRIS (%)")]
        public decimal Nr_PercentualGRIS { get; set; }

        [Display(Name = "GRIS Minímo (R$)")]
        public decimal Nr_ValorMinimoGRIS { get; set; }

        [Display(Name = "AdValorem (%)")]
        public decimal Nr_PercentualAdValorem { get; set; }

        [Display(Name = "AdValorem Minímo (R$)")]
        public decimal Nr_ValorMinimoAdvalorem { get; set; }

        [Display(Name = "Balsa (%)")]
        public decimal Nr_PercentualBalsa { get; set; }

        [Display(Name = "Fluvial Minímo(R$)")]
        public decimal Nr_ValorMinimoFluvial { get; set; }

        [Display(Name = "Redespacho Fluvial (%)")]
        public decimal Nr_PercentualRedespachoFluvial { get; set; }

        [Display(Name = "Frete Adicional")]
        public decimal? Nr_FreteAdicional { get; set; }

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


    }
}

