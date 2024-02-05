using System;
namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioProdutoMediaPreco
    {
        public RelatorioProdutoMediaPreco()
        {

        }
        public int AvaliacaoId { get; set; }
        public int ProdutoCategoriaId { get; set; }
        public string ProdutoCategiria { get; set; }
        public string Produto { get; set; }
        public string ProdutoImagem { get; set; }
        public int QuantidadeTotal { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMedio { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal Percentual { get; set; }
        public int Ordem { get; set; }
    }
}
