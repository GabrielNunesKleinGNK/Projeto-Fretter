using Fretter.Domain.Helpers.Attributes;
using System;
namespace Fretter.Domain.Dto.Fatura
{
    public class DoccobFiltro
    {
        public DoccobFiltro()
        {
        }

        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string CNPJFilial { get; set; }
        public string Serie { get; set; }
        public string NotaFiscal { get; set; }
        public decimal ValorFrete { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string ConhecimentoNumero { get; set; }
        public string ConhecimentoSerie { get; set; }
    }
}
