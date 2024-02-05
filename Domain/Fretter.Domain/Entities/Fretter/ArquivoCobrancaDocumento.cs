using Fretter.Domain.Enum;
using Fretter.Domain.Helpers.Proceda.Entidades;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ArquivoCobrancaDocumento : EntityBase
    {
        #region "Construtores"
        private ArquivoCobrancaDocumento() { }
        public ArquivoCobrancaDocumento(int id, int arquivoCobrancaId, string filialEmissora, int tipo, 
                                        string serie, string numero, DateTime dataEmissao, DateTime dataVencimento,
                                        decimal valorTotal, string tipoCobranca, string cfop, string codigoAcessoNFe,
                                        string chaveAcessoNFe, string protocoloNFe)
		{
            this.Ativar();
            this.AtualizarDataCriacao();
			this.AtualizarId(id);
            AtualizarArquivoCobrancaId(arquivoCobrancaId);
            AtualizarFilialEmissora(filialEmissora);
            AtualizarTipo(tipo);
            AtualizarSerie(serie);
            AtualizarNumero(numero);
            AtualizarDataEmissao(dataEmissao);
            AtualizarDataVencimento(dataVencimento);
            AtualizarValorTotal(valorTotal);
            AtualizarTipoCobranca(tipoCobranca);
            AtualizarCFOP(cfop);
            AtualizarCodigoAcessoNFe(codigoAcessoNFe);
            AtualizarChaveAcessoNFe(chaveAcessoNFe);
            AtualizarProtocoloNFe(protocoloNFe);
        }

        public ArquivoCobrancaDocumento(DOCCOB_Cobranca cobranca)
        {
            this.Ativar();
            this.AtualizarDataCriacao();
            AtualizarFilialEmissora(cobranca.FilialEmissora);
            AtualizarTipo(cobranca.Tipo);
            AtualizarSerie(cobranca.Serie);
            AtualizarNumero(cobranca.Numero);
            AtualizarDataEmissao(cobranca.DataEmissao);
            AtualizarDataVencimento(cobranca.DataVencimento);
            AtualizarValorTotal(cobranca.ValorTotal);
            AtualizarTipoCobranca(cobranca.TipoCobranca);
            AtualizarCFOP(cobranca.CFOP);
            AtualizarCodigoAcessoNFe(cobranca.CodigoAcessoNFe);
            AtualizarChaveAcessoNFe(cobranca.ChaveAcessoNFe);
            AtualizarProtocoloNFe(cobranca.ProtocoloNFe);

            foreach (var conhecimento in cobranca.Conhecimentos)
            {
                var cobrancaDocumentoItem = new ArquivoCobrancaDocumentoItem(conhecimento);
                AdicionarCobrancaDocumentoItem(cobrancaDocumentoItem);
            }
        }

        #endregion

        #region "Propriedades"
        public int ArquivoCobrancaId { get; protected set; }
        public string FilialEmissora { get; protected set; }
        public int Tipo { get; protected set; }
        public string Serie { get; protected set; }
        public string Numero { get; protected set; }
        public DateTime DataEmissao { get; protected set; }
        public DateTime DataVencimento { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public string TipoCobranca { get; protected set; }
        public string CFOP { get; protected set; }
        public string CodigoAcessoNFe { get; protected set; }
        public string ChaveAcessoNFe { get; protected set; }
        public string ProtocoloNFe { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual ArquivoCobranca ArquivoCobranca { get; set; }
        public virtual List<ArquivoCobrancaDocumentoItem> CobrancaDocumentoItens { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarArquivoCobrancaId(int arquivoCobrancaId) => this.ArquivoCobrancaId = arquivoCobrancaId;
        public void AtualizarFilialEmissora(string filialEmissora) => this.FilialEmissora = filialEmissora;
        public void AtualizarTipo(int tipo) => this.Tipo = tipo;
        public void AtualizarSerie(string serie) => this.Serie = serie;
        public void AtualizarNumero(string numero) => this.Numero = numero;
        public void AtualizarDataEmissao(DateTime data) => this.DataEmissao = data;
        public void AtualizarDataVencimento(DateTime data) => this.DataVencimento = data;
        public void AtualizarValorTotal(decimal valor) => this.ValorTotal = valor;
        public void AtualizarTipoCobranca(string tipoCobranca) => this.TipoCobranca = tipoCobranca;
        public void AtualizarCFOP(string cfop) => this.CFOP = cfop;
        public void AtualizarCodigoAcessoNFe(string codigoAcessoNFe) => this.CodigoAcessoNFe = codigoAcessoNFe;
        public void AtualizarChaveAcessoNFe(string chaveAcessoNFe) => this.ChaveAcessoNFe = chaveAcessoNFe;
        public void AtualizarProtocoloNFe(string protocoloNFe) => this.ProtocoloNFe = protocoloNFe;


        private void AdicionarCobrancaDocumentoItem(ArquivoCobrancaDocumentoItem cobrancaDocumentoItem)
        {
            if (CobrancaDocumentoItens == null)
                CobrancaDocumentoItens = new List<ArquivoCobrancaDocumentoItem>();
            CobrancaDocumentoItens.Add(cobrancaDocumentoItem);
        }
        #endregion
    }
}      
