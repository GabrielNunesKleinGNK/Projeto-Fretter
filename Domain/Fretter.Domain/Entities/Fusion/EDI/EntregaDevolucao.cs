using Fretter.Domain.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucao : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucao(int Id, int EntregaId, int EntregaTransporteTipoId, string CodigoColeta,
            string CodigoRastreio, DateTime? Validade, string Observacao, DateTime? Inclusao,
            int EntregaDevolucaoStatus, bool? Processado, bool? Finalizado, string CodigoRetorno, string codigoEntregaSaidaItem)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaId = EntregaId;
            this.EntregaTransporteTipoId = EntregaTransporteTipoId;
            this.CodigoColeta = CodigoColeta;
            this.CodigoRastreio = CodigoRastreio;
            this.Validade = Validade;
            this.Observacao = Observacao;
            this.Inclusao = Inclusao;
            this.EntregaDevolucaoStatus = EntregaDevolucaoStatus;
            this.Processado = Processado;
            this.Finalizado = Finalizado;
            this.Ativo = Ativo;
            this.CodigoRetorno = CodigoRetorno;
            this.CodigoEntregaSaidaItem = codigoEntregaSaidaItem;
        }
        #endregion

        #region "Propriedades"
        public int EntregaId { get; protected set; }
        public int EntregaTransporteTipoId { get; protected set; }
        public string CodigoColeta { get; protected set; }
        public string CodigoRastreio { get; protected set; }
        public DateTime? Validade { get; protected set; }
        public string Observacao { get; protected set; }
        public DateTime? Inclusao { get; protected set; }
        public int EntregaDevolucaoStatus { get; protected set; }
        public bool? Processado { get; protected set; }
        public bool? Finalizado { get; protected set; }
        public DateTime? DataUltimaOcorrencia { get; protected set; }
        public string UltimaOcorrenciaCodigo { get; protected set; }
        public string UltimaOcorrencia { get; protected set; }
        public string CodigoRetorno { get; protected set; }
        public string CodigoEntregaSaidaItem { get; protected set; }
        public int? EntregaReversaId { get; protected set; }

        [DataTableColumnIgnore]
        [NotMapped]
        public int? UltimaOcorrenciaTipoId { get; protected set; }
        #endregion

        #region "Referencias"
        public EntregaDevolucaoStatus Status { get; set; }
        public List<EntregaDevolucaoOcorrencia> Ocorrencias { get; set; }
        public Entrega Entrega { get; set; }
        public Entrega EntregaReversa { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEntrega(int EntregaId) => this.EntregaId = EntregaId;
        public void AtualizarEntregaReversa(int? EntregaReversaId) => this.EntregaReversaId = EntregaReversaId;
        public void AtualizarEntregaTransporteTipoId(int EntregaTransporteTipoId) => this.EntregaTransporteTipoId = EntregaTransporteTipoId;
        public void AtualizarCodigoColeta(string CodigoColeta) => this.CodigoColeta = CodigoColeta;
        public void AtualizarCodigoRastreio(string CodigoRastreio) => this.CodigoRastreio = CodigoRastreio;
        public void AtualizarValidade(DateTime? Validade) => this.Validade = Validade;
        public void AtualizarObservacao(string Observacao) => this.Observacao = Observacao;
        public void AtualizarInclusao(DateTime? Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarUltimaOcorrencia(string UltimaOcorrencia) => this.UltimaOcorrencia = UltimaOcorrencia;
        public void AtualizarUltimaOcorrenciaCodigo(string UltimaOcorrenciaCodigo) => this.UltimaOcorrenciaCodigo = UltimaOcorrenciaCodigo;
        public void AtualizarEntregaDevolucaoStatus(int EntregaDevolucaoStatus) => this.EntregaDevolucaoStatus = EntregaDevolucaoStatus;
        public void AtualizarProcessado(bool? Processado) => this.Processado = Processado;
        public void AtualizarUltimaOcorrenciaTipo(int ultimaOcorrenciaTipo) => this.UltimaOcorrenciaTipoId = ultimaOcorrenciaTipo;
        public void AtualizarCodigoEntregaSaida(string codigoEntregaSaida) => this.CodigoEntregaSaidaItem = codigoEntregaSaida;
        #endregion
    }
}
