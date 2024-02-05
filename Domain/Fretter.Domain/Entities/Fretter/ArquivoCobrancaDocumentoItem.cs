using Fretter.Domain.Enum;
using Fretter.Domain.Helpers.Proceda.Entidades;
using System;

namespace Fretter.Domain.Entities
{
    public class ArquivoCobrancaDocumentoItem : EntityBase
    {
        #region "Construtores"
        private ArquivoCobrancaDocumentoItem() { }
        public ArquivoCobrancaDocumentoItem(int id, int arquivoCobrancaDocumentoId, string filial, string serie, 
                                        string numero, DateTime dataEmissao, decimal valorFrete, string documentoRemetente,
                                        string documentoDestinatario, string documentoEmissor, string ufEmbarcadora,
                                        string ufDestinataria, string ufEmissora, bool devolucao, string codigoIva)
		{
            this.Ativar();
            this.AtualizarDataCriacao();
			this.AtualizarId(id);
            AtualizarArquivoCobrancaDocumentoId(arquivoCobrancaDocumentoId);
            AtualizarFilial(filial);
            AtualizarSerie(serie);
            AtualizarNumero(numero);
            AtualizarValorFrete(valorFrete);
            AtualizarDataEmissao(dataEmissao);
            AtualizarDocumentoRemetente(documentoRemetente);
            AtualizarDocumentoDestinatario(documentoDestinatario);
            AtualizarDocumentoEmissor(documentoEmissor);
            AtualizarUfEmbarcadora(ufEmbarcadora);
            AtualizarUfDestinataria(ufDestinataria);
            AtualizarUfEmissora(ufEmissora);
            AtualizarDevolucao(devolucao);
            AtualizarCodigoIVA(codigoIva);
        }


        public ArquivoCobrancaDocumentoItem(DOCCOB_Conhecimento conhecimento)
        {
            this.Ativar();
            AtualizarFilial(conhecimento.Filial);
            AtualizarSerie(conhecimento.Serie);
            AtualizarNumero(conhecimento.Numero);
            AtualizarValorFrete(conhecimento.ValorFrete);
            AtualizarDataEmissao(conhecimento.DataEmissao);
            AtualizarDocumentoRemetente(conhecimento.DocumentoRemetente);
            AtualizarDocumentoDestinatario(conhecimento.DocumentoDestinatario);
            AtualizarDocumentoEmissor(conhecimento.DocumentoEmissor);
            AtualizarUfEmbarcadora(conhecimento.UfEmbarcadora);
            AtualizarUfDestinataria(conhecimento.UfDestinataria);
            AtualizarUfEmissora(conhecimento.UfEmissora);
            AtualizarDevolucao(conhecimento.Devolucao.ToLower() == "s" ? true : false);
            AtualizarCodigoIVA(conhecimento.CodigoIVA);
        }
        #endregion

        #region "Propriedades"
        public int ArquivoCobrancaDocumentoId { get; protected set; }
        public string Filial { get; protected set; }
        public string Serie { get; protected set; }
        public string Numero { get; protected set; }
        public decimal ValorFrete { get; protected set; }
        public DateTime DataEmissao { get; protected set; }
        public string DocumentoRemetente { get; protected set; }
        public string DocumentoDestinatario { get; protected set; }
        public string DocumentoEmissor { get; protected set; }
        public string UfEmbarcadora { get; protected set; }
        public string UfDestinataria { get; protected set; }
        public string UfEmissora { get; protected set; }
        public string CodigoIVA { get; protected set; }
        public bool Devolucao { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual ArquivoCobrancaDocumento ArquivoCobrancaDocumento { get; set; }
        #endregion

        #region "Métodos"
        private void AtualizarCodigoIVA(string codigoIva) => this.CodigoIVA = codigoIva;
        private void AtualizarValorFrete(decimal valorFrete) => this.ValorFrete = valorFrete;
        public void AtualizarArquivoCobrancaDocumentoId(int arquivoCobrancaDocumentoId) => this.ArquivoCobrancaDocumentoId = arquivoCobrancaDocumentoId;
        public void AtualizarFilial(string filial) => this.Filial = filial;
        public void AtualizarSerie(string serie) => this.Serie = serie;
        public void AtualizarNumero(string numero) => this.Numero = numero;
        public void AtualizarDataEmissao(DateTime data) => this.DataEmissao = data;
        public void AtualizarDocumentoRemetente(string documento) => this.DocumentoRemetente = documento;
        public void AtualizarDocumentoDestinatario(string documento) => this.DocumentoDestinatario = documento;
        public void AtualizarDocumentoEmissor(string documento) => this.DocumentoEmissor = documento;
        public void AtualizarUfEmbarcadora(string uf) => this.UfEmbarcadora = uf;
        public void AtualizarUfDestinataria(string uf) => this.UfDestinataria = uf;
        public void AtualizarUfEmissora(string uf) => this.UfEmissora = uf;
        public void AtualizarDevolucao(bool devolucao) => this.Devolucao = devolucao;
        #endregion
    }
}      
