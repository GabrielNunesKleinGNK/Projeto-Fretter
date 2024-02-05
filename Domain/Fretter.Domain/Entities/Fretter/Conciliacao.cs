
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class Conciliacao : EntityBase
    {
        #region "Construtores"
        public Conciliacao(int Id, int? EmpresaId, int? EntregaId, int? TransportadorId, decimal? ValorCustoFrete, decimal? ValorCustoAdicional, decimal? ValorCustoReal, decimal? ValorCustoDivergencia, int? QuantidadeTentativas, bool? PossuiDivergenciaPeso, bool? PossuiDivergenciaTarifa, bool? DevolvidoRemetente, int ConciliacaoStatusId,
            DateTime? DataEmissao, DateTime? DataFinalizacao, int? FaturaId, bool? ProcessadoIndicador, string JsonValoresRecotacao, string JsonValoresCte, int? ImportacaoCteId, int? ConciliacaoTipoId)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.EntregaId = EntregaId;
            this.TransportadorId = TransportadorId;
            this.ValorCustoFrete = ValorCustoFrete;
            this.ValorCustoAdicional = ValorCustoAdicional;
            this.ValorCustoReal = ValorCustoReal;
            this.ValorCustoDivergencia = ValorCustoDivergencia;
            this.QuantidadeTentativas = QuantidadeTentativas;
            this.PossuiDivergenciaPeso = PossuiDivergenciaPeso;
            this.PossuiDivergenciaTarifa = PossuiDivergenciaTarifa;
            this.DevolvidoRemetente = DevolvidoRemetente;
            this.ConciliacaoStatusId = ConciliacaoStatusId;
            this.DataEmissao = DataEmissao;
            this.DataFinalizacao = DataFinalizacao;
            this.FaturaId = FaturaId;
            this.ProcessadoIndicador = ProcessadoIndicador;
            this.JsonValoresRecotacao = JsonValoresRecotacao;
            this.JsonValoresCte = JsonValoresCte;
            this.ImportacaoCteId = ImportacaoCteId;
            this.ConciliacaoTipoId = ConciliacaoTipoId;
        }
        #endregion

        #region "Propriedades"
        public int? EmpresaId { get; protected set; }
        public int? EntregaId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        public decimal? ValorCustoFrete { get; protected set; }
        public decimal? ValorCustoAdicional { get; protected set; }
        public decimal? ValorCustoReal { get; protected set; }
        public decimal? ValorCustoDivergencia { get; protected set; }
        public int? QuantidadeTentativas { get; protected set; }
        public bool? PossuiDivergenciaPeso { get; protected set; }
        public bool? PossuiDivergenciaTarifa { get; protected set; }
        public bool? DevolvidoRemetente { get; protected set; }
        public int ConciliacaoStatusId { get; protected set; }
        public DateTime? DataEmissao { get; protected set; }
        public DateTime? DataFinalizacao { get; protected set; }
        public int? FaturaId { get; protected set; }
        public bool? ProcessadoIndicador { get; protected set; }
        public string JsonValoresRecotacao { get; protected set; }
        public string JsonValoresCte { get; protected set; }
        public int? ImportacaoCteId { get; protected set; }
        public int? ConciliacaoTipoId { get; protected set; }
        #endregion

        #region "Referencias"

        public virtual ImportacaoCte ImportacaoCte { get; set; }
        public virtual Entrega Entrega { get; set; }
        public virtual List<FaturaConciliacao> FaturaConciliacoes { get; set; }
        public virtual ConciliacaoTipo ConciliacaoTipo { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaId(int? EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarEntregaId(int? EntregaId) => this.EntregaId = EntregaId;
        public void AtualizarTransportadorId(int? TransportadorId) => this.TransportadorId = TransportadorId;
        public void AtualizarValorCustoFrete(decimal? ValorCustoFrete) => this.ValorCustoFrete = ValorCustoFrete;
        public void AtualizarValorCustoAdicional(decimal? ValorCustoAdicional) => this.ValorCustoAdicional = ValorCustoAdicional;
        public void AtualizarValorCustoReal(decimal? ValorCustoReal) => this.ValorCustoReal = ValorCustoReal;
        public void AtualizarValorCustoDivergencia(decimal? ValorCustoDivergencia) => this.ValorCustoDivergencia = ValorCustoDivergencia;
        public void AtualizarQuantidadeTentativas(int? QuantidadeTentativas) => this.QuantidadeTentativas = QuantidadeTentativas;
        public void AtualizarPossuiDivergenciaPeso(bool? PossuiDivergenciaPeso) => this.PossuiDivergenciaPeso = PossuiDivergenciaPeso;
        public void AtualizarPossuiDivergenciaTarifa(bool? PossuiDivergenciaTarifa) => this.PossuiDivergenciaTarifa = PossuiDivergenciaTarifa;
        public void AtualizarDevolvidoRemetente(bool? DevolvidoRemetente) => this.DevolvidoRemetente = DevolvidoRemetente;
        public void AtualizarConciliacaoStatusId(int ConciliacaoStatusId) => this.ConciliacaoStatusId = ConciliacaoStatusId;
        public void AtualizarDataEmissao(DateTime? DataEmissao) => this.DataEmissao = DataEmissao;
        public void AtualizarDataFinalizacao(DateTime? DataFinalizacao) => this.DataFinalizacao = DataFinalizacao;
        public void AtualizarFaturaId(int? FaturaId) => this.FaturaId = FaturaId;
        public void AtualizarProcessadoIndicador(bool? ProcessadoIndicador) => this.ProcessadoIndicador = ProcessadoIndicador;
        public void AtualizarJsonValoresRecotacao(string JsonValoresRecotacao) => this.JsonValoresRecotacao = JsonValoresRecotacao;
        public void AtualizarJsonValoresCte(string JsonValoresCte) => this.JsonValoresCte = JsonValoresCte;
        public void AtualizarImportacaoCteId(int? ImportacaoCteId) => this.ImportacaoCteId = ImportacaoCteId;
        public void AtualizarConciliacaoTipoId(int? ConciliacaoTipoId) => this.ConciliacaoTipoId = ConciliacaoTipoId;
        #endregion
    }
}
