using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaHistoricoViewModel : IViewModel<FaturaHistorico>
    {
        public int Id { get; set; }
        public int? FaturaId { get;  set; }
        public int? FaturaStatusId { get;  set; }
        public string Descricao { get;  set; }
        public decimal ValorCustoFrete { get;  set; }
        public decimal ValorCustoAdicional { get;  set; }
        public decimal ValorCustoReal { get;  set; }
        public int? QuantidadeEntrega { get;  set; }
        public int? QuantidadeSucesso { get;  set; }
        public int? QuantidadeDivergencia { get;  set; }
        public DateTime? DataCadastro { get; set; }
        public int? FaturaStatusAnteriorId { get; set; }

        public FaturaStatusViewModel FaturaStatus { get; set; }
        public FaturaStatusViewModel FaturaStatusAnterior { get; set; }
        //public FaturaViewModel Fatura{ get; set; }

        public FaturaHistorico Model()
		{
			return new FaturaHistorico(Id, FaturaId, FaturaStatusId, Descricao, ValorCustoFrete, ValorCustoAdicional, ValorCustoReal, QuantidadeEntrega, QuantidadeSucesso);
		}
    }
}      
