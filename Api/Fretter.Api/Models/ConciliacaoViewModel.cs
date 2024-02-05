
using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConciliacaoViewModel : IViewModel<Conciliacao>
    {
        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? EntregaId { get; set; }
        public int? TransportadorId { get; set; }
        public decimal? ValorCustoFrete { get; set; }
        public decimal? ValorCustoAdicional { get; set; }
        public decimal? ValorCustoReal { get; set; }
        public decimal? ValorCustoDivergencia { get; set; }
        public int? QuantidadeTentativas { get; set; }
        public bool? PossuiDivergenciaPeso { get; set; }
        public bool? PossuiDivergenciaTarifa { get; set; }
        public bool? DevolvidoRemetente { get; set; }
        public int ConciliacaoStatusId { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataEmissao { get; protected set; }
        public DateTime? DataFinalizacao { get; protected set; }
        public int? FaturaId { get; protected set; }
        public bool? ProcessadoIndicador { get; protected set; }
        public string JsonValoresRecotacao { get; protected set; }
        public string JsonValoresCte { get; protected set; }
        public int? ImportacaoCteId { get; protected set; }
        public int? ConciliacaoTipoId { get; protected set; }

        public Conciliacao Model()
        {
            return new Conciliacao(Id, EmpresaId, EntregaId, TransportadorId, ValorCustoFrete, ValorCustoAdicional, ValorCustoReal, ValorCustoDivergencia, QuantidadeTentativas, PossuiDivergenciaPeso, PossuiDivergenciaTarifa, DevolvidoRemetente, ConciliacaoStatusId,
                DataEmissao, DataFinalizacao, FaturaId, ProcessadoIndicador, JsonValoresRecotacao, JsonValoresCte, ImportacaoCteId, ConciliacaoTipoId);
        }
    }
}
