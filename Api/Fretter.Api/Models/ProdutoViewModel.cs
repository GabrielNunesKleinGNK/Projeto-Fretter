using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;

namespace Fretter.Api.Models
{
    public class ProdutoViewModel : IViewModel<Produto>
    {
		public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public decimal PesoLiq { get; set; }
        public decimal Largura { get;  set; }
        public decimal Altura { get;  set; }
        public decimal Comprimento { get;  set; }

        public Produto Model()
		{
			return new Produto(Id, EmpresaId, Codigo, Nome, Preco, PesoLiq, Largura, Altura, Comprimento);
		}
    }
}      
