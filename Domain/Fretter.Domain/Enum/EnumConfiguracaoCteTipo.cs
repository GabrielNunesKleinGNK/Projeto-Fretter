using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Enum
{
    public enum EnumConfiguracaoCteTipo
    {
        [Display(Name = "Peso")]
        Peso = 1,
        [Display(Name = "Valor")]
        Valor,
        [Display(Name = "Icms")]
        Icms,
        [Display(Name = "Gris")]
        Gris,
        [Display(Name = "Advalorem")]
        Advalorem,
        [Display(Name = "Pedagio")]
        Pedagio,
        [Display(Name = "FretePeso")]
        FretePeso,
        [Display(Name = "TaxaTRT")]
        TaxaTRT,
        [Display(Name = "TaxaTDE")]
        TaxaTDE,
        [Display(Name = "TaxaTDA")]
        TaxaTDA,
        [Display(Name = "TaxaCtE")]
        TaxaCtE,
        [Display(Name = "TaxaRisco")]
        TaxaRisco,
        [Display(Name = "Suframa")]
        Suframa,
        [Display(Name = "PesoConsiderado")]
        PesoConsiderado
    }

}
