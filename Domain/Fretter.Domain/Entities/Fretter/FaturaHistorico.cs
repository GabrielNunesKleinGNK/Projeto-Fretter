using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaHistorico : EntityBase
    {
        #region "Construtores"
        protected FaturaHistorico() { }
        public FaturaHistorico(int Id, int? faturaId, int? status, string descricao, decimal custoFrete, decimal custoAdicional, decimal custoReal, int? quantidadeEntregas, int? quantidadeSucesso)
        {

            this.Ativar();
            this.Id = Id;
            this.FaturaId = faturaId;
            this.FaturaStatusId = status;
            this.Descricao = descricao;
            this.ValorCustoFrete = custoFrete;
            this.ValorCustoAdicional = custoAdicional;
            this.ValorCustoReal = custoReal;
            this.QuantidadeEntrega = quantidadeEntregas;
            this.QuantidadeSucesso = quantidadeSucesso;
        }

        public FaturaHistorico(int? faturaId, int? status, string descricao, decimal custoFrete, decimal custoAdicional, decimal custoReal, int? quantidadeEntregas, int quantidadeSucesso)
        {
            AtualizarFaturaId(faturaId);
            AtualizarStatusId(status);
            AtualizarDescricao(descricao);
            AtualizarCustoFrete(custoFrete);
            AtualizarCustoAdicional(custoAdicional);
            AtualizarCustoReal(custoReal);
            AtualizarQuantidadeEntregas(quantidadeEntregas);
            AtualizarQuantidadeSucesso(quantidadeSucesso);
            AtualizarDataCriacao();
            Ativar();
        }

        public FaturaHistorico(Fatura fatura, int usuarioId, string descricao, int? statusAtual = null)
        {
            //TODO criar fatura historico a partir de uma fatura
            AtualizarFaturaId(fatura.Id);
            AtualizarStatusId(statusAtual);
            AtualizarDescricao(descricao);
            AtualizarCustoFrete(fatura.ValorCustoFrete);
            AtualizarCustoAdicional(fatura.ValorCustoAdicional);
            AtualizarCustoReal(fatura.ValorCustoReal);
            AtualizarQuantidadeEntregas(fatura.QuantidadeEntrega);
            AtualizarQuantidadeSucesso(fatura.QuantidadeSucesso);
            AtualizarUsuarioCriacao(usuarioId);
            AtualizarStatusAnteriorId(fatura.FaturaStatusId);
            AtualizarDataCriacao();
            Ativar();
        }

        #endregion

        #region "Propriedades"
        public int? FaturaId { get; protected set; }
        public int? FaturaStatusId { get; protected set; }
        public string Descricao { get; protected set; }
        public decimal? ValorCustoFrete { get; protected set; }
        public decimal? ValorCustoAdicional { get; protected set; }
        public decimal? ValorCustoReal { get; protected set; }
        public int? QuantidadeEntrega { get; protected set; }
        public int? QuantidadeSucesso { get; protected set; }
        public int? FaturaStatusAnteriorId { get; protected set; }

        #endregion

        #region "Referencias"
        public virtual FaturaStatus FaturaStatus { get; set; }
        public virtual FaturaStatus FaturaStatusAnterior { get; set; }
        //public virtual Fatura Fatura { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarFaturaId(int? faturaId) => this.FaturaId = faturaId;
        public void AtualizarStatusId(int? status) => this.FaturaStatusId = status.GetHashCode();
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        public void AtualizarCustoFrete(decimal? custoFrete) => this.ValorCustoFrete = custoFrete;
        public void AtualizarCustoAdicional(decimal? custoAdicional) => this.ValorCustoAdicional = custoAdicional;
        public void AtualizarCustoReal(decimal? custoReal) => this.ValorCustoReal = custoReal;
        public void AtualizarQuantidadeEntregas(int? quantidadeEntregas) => this.QuantidadeEntrega = quantidadeEntregas;
        public void AtualizarQuantidadeSucesso(int? quantidadeSucesso) => this.QuantidadeSucesso = quantidadeSucesso;
        public void AtualizarStatusAnteriorId(int? status) => this.FaturaStatusAnteriorId = status;
        #endregion
    }
}
