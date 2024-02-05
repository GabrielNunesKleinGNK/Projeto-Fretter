using Fretter.Domain.Helpers.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class TabelaPrecoModel
    {
        [DataTableColumnIgnore]
        [Display(Name = "Código", ShortName = "Cod.")]
        public int Cd_Id { get; set; }

        [Display(Name = "Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataTableColumnIgnore]
        public int? Id_Tabela { get; set; }

        [Display(Name = "Região")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id_Regiao { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id_TabelaPeso { get; set; }

        [DataTableColumnIgnore]
        public decimal Nr_PesoFim { get; set; }

        [Display(Name = "Valor")]
        public decimal Nr_Valor { get; set; }
    }
}
