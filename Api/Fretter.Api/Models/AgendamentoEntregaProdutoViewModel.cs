using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class AgendamentoEntregaProdutoViewModel : IViewModel<AgendamentoEntregaProduto>
    {
        public int Id { get; set; }
        public int? EntregaId { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Nome { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal ValorTotal => ValorProduto;
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Peso { get; set; }

        public AgendamentoEntregaProduto Model()
        {
            return new AgendamentoEntregaProduto(Id, EntregaId, SKU, EAN, Nome, ValorProduto, ValorTotal, Altura, Largura, Comprimento, Peso);
        }
    }
}
