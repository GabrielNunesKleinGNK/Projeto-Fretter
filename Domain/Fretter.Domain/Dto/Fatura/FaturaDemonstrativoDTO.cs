using Fretter.Domain.Dto.Fretter.Conciliacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaDemonstrativoDTO : DemonstrativoDTO
    {
        public FaturaDemonstrativoDTO() { }
        public FaturaDemonstrativoDTO(Entities.Fatura fatura, DemonstrativoRetorno conciliacao):base(conciliacao)
        {
            Codigo = fatura.Id;
            Transportador = fatura.Transportador.Nome;
            Emissao = fatura.DataCadastro;
            Vencimento = fatura.DataVencimento;
            QuantidadeEntregas = fatura.QuantidadeEntrega;
            QuantidadeItensDoccob = fatura.QuantidadeSucesso;
            CustoFrete = fatura.ValorCustoFrete;
            CustoReal = fatura.ValorCustoReal;
            Status = fatura.FaturaStatus.Descricao;
        }

        public int Codigo { get; set; }
        public string Transportador { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime? Vencimento { get; set; }
        public int? QuantidadeEntregas { get; set; }
        public int? QuantidadeItensDoccob { get; set; }
        public decimal? CustoFrete { get; set; }
        public decimal? CustoReal { get; set; }
        public string Status { get; set; }

        public static new Dictionary<int, string> GetCustomColor()
        {
            var customColor = new Dictionary<int, string>();
            var i = 1;
            foreach (PropertyInfo prop in typeof(FaturaDemonstrativoDTO).GetProperties())
            {
                if (prop.Name.StartsWith("CTE"))
                    customColor.Add(i++, CteColor);
                else if (prop.Name.StartsWith("Diferenca"))
                    customColor.Add(i++, DiferencaColor);
                else
                    customColor.Add(i++, PadraoColor);
            }

            return customColor;
        }
    }
}
