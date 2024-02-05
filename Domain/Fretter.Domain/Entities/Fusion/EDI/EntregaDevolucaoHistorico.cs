using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucaoHistorico : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucaoHistorico(int Id, int EntregaDevolucaoId, string CodigoColeta, string CodigoRastreio,
            DateTime? Validade, string Mensagem, string Retorno, DateTime? Inclusao, int EntregaDevolucaoStatusAnteriorId, int EntregaDevolucaoStatusAtualId, string XmlRetorno)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaDevolucaoId = EntregaDevolucaoId;
            this.CodigoColeta = CodigoColeta;
            this.CodigoRastreio = CodigoRastreio;
            this.Validade = Validade;
            this.Mensagem = Mensagem;
            this.Retorno = Retorno;
            this.EntregaDevolucaoStatusAnteriorId = EntregaDevolucaoStatusAnteriorId;
            this.EntregaDevolucaoStatusAtualId = EntregaDevolucaoStatusAtualId;
            this.Inclusao = Inclusao;
            this.XmlRetorno = XmlRetorno;
        }
        #endregion

        #region "Propriedades"
        public int EntregaDevolucaoId { get; protected set; }
        public string CodigoColeta { get; protected set; }
        public string CodigoRastreio { get; protected set; }
        public DateTime? Validade { get; protected set; }
        public string Mensagem { get; protected set; }
        public string Retorno { get; protected set; }
        public int EntregaDevolucaoStatusAnteriorId { get; protected set; }
        public int EntregaDevolucaoStatusAtualId { get; protected set; }
        public DateTime? Inclusao { get; protected set; }
        public string XmlRetorno { get; protected set; }
        #endregion

        #region "Referencias"
        public EntregaDevolucaoStatus StatusAtual { get; set; }
        public EntregaDevolucaoStatus StatusAnterior { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEntregaDevolucaoId(int EntregaDevolucaoId) => this.EntregaDevolucaoId = EntregaDevolucaoId;
        public void AtualizarCodigoColeta(string CodigoColeta) => this.CodigoColeta = CodigoColeta;
        public void AtualizarCodigoRastreio(string CodigoRastreio) => this.CodigoRastreio = CodigoRastreio;
        public void AtualizarValidade(DateTime? Validade) => this.Validade = Validade;
        public void AtualizarMensagem(string Mensagem) => this.Mensagem = Mensagem;
        public void AtualizarRetorno(string Retorno) => this.Retorno = Retorno;
        public void AtualizarInclusao(DateTime? Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarEntregaDevolucaoStatusAnteriorId(int EntregaDevolucaoStatusAnteriorId) => this.EntregaDevolucaoStatusAnteriorId = EntregaDevolucaoStatusAnteriorId;
        public void AtualizarEntregaDevolucaoStatusAtualId(int EntregaDevolucaoStatusAtualId) => this.EntregaDevolucaoStatusAtualId = EntregaDevolucaoStatusAtualId;
        public void AtualizarUsuarioCriacao(int UsuarioCadastro) => this.UsuarioCadastro = UsuarioCadastro;
        #endregion
    }
}
