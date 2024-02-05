using System;
using System.Collections.Generic;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaViewModel : IViewModel<Fatura>
    {
        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? TransportadorId { get; set; }
        public int? FaturaPeriodoId { get; set; }
        public decimal ValorCustoFrete { get; set; }
        public decimal ValorCustoAdicional { get; set; }
        public decimal ValorCustoReal { get; set; }
        public decimal ValorDocumento { get; set; }
        public int QuantidadeDevolvidoRemetente { get; set; }
        public int FaturaStatusId { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int? QuantidadeSucesso { get; set; }
        public int? QuantidadeEntrega { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? QuantidadeDivergencia { get; set; }

        public FaturaStatusViewModel FaturaStatus { get; set; }
        public FaturaPeriodoViewModel FaturaPeriodo { get; set; }
        public List<ArquivoCobrancaViewModel> ArquivoCobrancas { get; set; }
        public Fusion.TransportadorCnpjViewModel Transportador { get; set; }

        //Campos computados
        public decimal ValorTotalDoccob { get; set; }
        public int QtdTotalDoccob { get; set; }
        public int QtdTotalItensDoccob { get; set; }
        public Fatura Model()
        {
            return new Fatura(Id, EmpresaId, TransportadorId, FaturaPeriodoId, ValorCustoFrete,
            ValorCustoAdicional, ValorCustoReal, QuantidadeDevolvidoRemetente, FaturaStatusId, DataVencimento, QuantidadeSucesso, QuantidadeEntrega,ValorDocumento);
        }
    }
}
