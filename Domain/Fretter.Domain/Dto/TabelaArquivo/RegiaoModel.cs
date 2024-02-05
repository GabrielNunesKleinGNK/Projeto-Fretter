using Fretter.Domain.Helpers.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoModel
    {
        [Display(Name = "Código", ShortName = "Cod.")]
        public int Cd_Id { get; set; }

        [Display(Name = "Empresa")]
        [DataTableColumnIgnore]
        public int Id_Empresa { get; set; }

        [Display(Name = "Incluido em")]
        [DataTableColumnIgnore]
        public DateTime Dt_Inclusao { get; set; }

        [Display(Name = "Transportador")]
        [DataTableColumnIgnore]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id_Transportador { get; set; }

        [Display(Name = "Tabela")]
        [DataTableColumnIgnore]
        public int? Id_Tabela { get; set; }
        [DataTableColumnIgnore]
        public string TabelaNome { get; set; }

        [Display(Name = "Tipo de Região")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id_RegiaoTipo { get; set; }
        public string Ds_Tipo { get; set; }

        [Display(Name = "Nome", ShortName = "Nome")]
        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Text)]
        public string Ds_Regiao { get; set; }

        [Display(Name = "Prazo")]
        public byte Nr_Prazo { get; set; }

        [Display(Name = "UF")]
        public string Cd_Uf { get; set; }

        [Display(Name = "Ativo")]
        public bool Flg_Ativo { get; set; }

        [Display(Name = "Genérica")]
        public bool Flg_Generica { get; set; }

        [Display(Name = "Peso Excedente")]
        public decimal Nr_PesoExcedente { get; set; }

        [Display(Name = "Peso Máximo")]
        public decimal Nr_PesoMaximo { get; set; }

        [Display(Name = "Pedágio Fixo")]
        public decimal Nr_PedagioFixo { get; set; }

        [Display(Name = "Pedágio Variável")]
        public decimal Nr_PedagioVariavel { get; set; }

        [Display(Name = "Fração Peso Pedágio")]
        public decimal Nr_FracaoPesoPedagio { get; set; }

        [Display(Name = "GRIS (%)")]
        public decimal Nr_PercentualGRIS { get; set; }

        
        [Display(Name = "AdValorem (%)")]
        public decimal Nr_PercentualAdValorem { get; set; }

        
        [Display(Name = "Balsa (%)")]
        public decimal Nr_PercentualBalsa { get; set; }

        [Display(Name = "Redespacho Fluvial (%)")]
        public decimal Nr_PercentualRedespachoFluvial { get; set; }
               
        [Display(Name = "Fator Cubagem")]
        public int? Nr_FatorCubagem { get; set; }

    }
}

