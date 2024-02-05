using Fretter.Domain.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoCEPModel
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
    }
}
