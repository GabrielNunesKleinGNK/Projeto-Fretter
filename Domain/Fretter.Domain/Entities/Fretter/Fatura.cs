using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class Fatura : EntityBase
    {
        #region "Construtores"
        protected Fatura() { }
        public Fatura(int Id, int? EmpresaId, int? TransportadorId, int? FaturaPeriodoId, decimal ValorCustoFrete,
                      decimal ValorCustoAdicional, decimal ValorCustoReal, int QuantidadeDevolvidoRemetente, int FaturaStatusId,
                      DateTime? DataVencimento, int? QuantidadeSucesso, int? QuantidadeEntrega, decimal? ValorDocumento)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.TransportadorId = TransportadorId;
            this.FaturaPeriodoId = FaturaPeriodoId;
            this.ValorCustoFrete = ValorCustoFrete;
            this.ValorCustoAdicional = ValorCustoAdicional;
            this.ValorCustoReal = ValorCustoReal;
            this.QuantidadeDevolvidoRemetente = QuantidadeDevolvidoRemetente;
            this.FaturaStatusId = FaturaStatusId;
            this.DataVencimento = DataVencimento;
            this.QuantidadeSucesso = QuantidadeSucesso;
            this.QuantidadeEntrega = QuantidadeEntrega;
            this.ValorDocumento = ValorDocumento;
        }

        public Fatura(DateTime dataVencimento, int? empresaId, int? transportadorId, int? faturaPeriodoId, decimal custoFrete, decimal custoAdicional, decimal custoReal,
                      EnumFaturaStatus status, int? QuantidadeSucesso, int? QuantidadeEntrega)
        {
            AtualizarDataVencimento(dataVencimento);
            AtualizarEmpresaId(empresaId);
            AtualizarTransportadorId(transportadorId);
            AtualizarFaturaPeriodoId(faturaPeriodoId);
            AtualizarCustoFrete(custoFrete);
            AtualizarCustoAdicional(custoAdicional);
            AtualizarCustoReal(custoReal);
            AtualizarStatusId(status);
            AtualizarQuantidadeSucesso(QuantidadeSucesso);
            AtualizarQuantidadeEntrega(QuantidadeEntrega);
            AtualizarDataCriacao();
            Ativar();
        }

        #endregion

        #region "Propriedades"
        public int? EmpresaId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        public int? FaturaPeriodoId { get; protected set; }
        public decimal? ValorCustoFrete { get; protected set; }
        public decimal? ValorCustoAdicional { get; protected set; }
        public decimal? ValorCustoReal { get; protected set; }
        public decimal? ValorDocumento { get; protected set; }
        public int? QuantidadeDevolvidoRemetente { get; protected set; }
        public int FaturaStatusId { get; protected set; }
        public DateTime? DataVencimento { get; protected set; }
        public int? QuantidadeSucesso { get; protected set; }
        public int? QuantidadeEntrega { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual FaturaStatus FaturaStatus { get; set; }
        public virtual FaturaPeriodo FaturaPeriodo { get; set; }
        public virtual TransportadorCnpj Transportador { get; set; }
        public virtual List<ArquivoCobranca> ArquivoCobrancas { get; set; }
        public virtual List<FaturaConciliacao> FaturaConciliacoes { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarDataVencimento(DateTime dataVencimento) => this.DataVencimento = dataVencimento;
        public void AtualizarEmpresaId(int? empresaId) => this.EmpresaId = empresaId;
        public void AtualizarTransportadorId(int? transportadorId) => this.TransportadorId = transportadorId;
        public void AtualizarFaturaPeriodoId(int? faturaPeriodoId) => this.FaturaPeriodoId = faturaPeriodoId;
        public void AtualizarCustoFrete(decimal custoFrete) => this.ValorCustoFrete = custoFrete;
        public void AtualizarCustoAdicional(decimal custoAdicional) => this.ValorCustoAdicional = custoAdicional;
        public void AtualizarCustoReal(decimal custoReal) => this.ValorCustoReal = custoReal;
        public void AtualizarValorDocumento(decimal valorDocumento) => this.ValorDocumento = valorDocumento;
        public void AtualizarStatusId(EnumFaturaStatus status) => this.FaturaStatusId = status.GetHashCode();
        public void AtualizarStatusId(int status) => this.FaturaStatusId = status;
        public void AtualizarQuantidadeSucesso(int? quantidadeSucesso) => this.QuantidadeSucesso = quantidadeSucesso;
        public void AtualizarQuantidadeEntrega(int? quantidadeEntrega) => this.QuantidadeEntrega = quantidadeEntrega;

        #endregion
    }
}
