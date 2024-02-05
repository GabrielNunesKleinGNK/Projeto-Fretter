using Fretter.Domain.Enum;
using Fretter.Domain.Helpers.Proceda.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Domain.Entities
{
    public class ArquivoCobranca : EntityBase
    {
        #region "Construtores"
        private ArquivoCobranca() { }
        public ArquivoCobranca(int id, int faturaId, string identificacaoRemetente, string identificacaoDestinatario, DateTime data, 
                               int qtdTotal, string arquivoUrl, decimal valorTotal, int qtdItens)
		{
            this.Ativar();
            this.AtualizarDataCriacao();
			this.AtualizarId(id);
            AtualizarFaturaId(faturaId);
            AtualizarIdentificacaoRemetente(identificacaoRemetente);
            AtualizarIdentificacaoDestinatario(identificacaoDestinatario);
            AtualizarData(data);
            AtualizarQtdTotal(qtdTotal);
            AtualizarValorTotal(valorTotal);
            AtualizarArquivoUrl(arquivoUrl);
            AtualizarQtdItens(qtdItens);
        }

        public ArquivoCobranca(DOCCOB dto, int faturaId, int empresaId, string arquivoUrl)
        {
            this.Ativar();
            this.AtualizarDataCriacao();
            AtualizarFaturaId(faturaId);
            AtualizarIdentificacaoRemetente(dto.Id_Remetente);
            AtualizarIdentificacaoDestinatario(dto.Id_Destinatario);
            AtualizarData(dto.Data);
            AtualizarArquivoUrl(arquivoUrl);
            int total = 0;
            int totalItens = 0;
            decimal soma = 0;
            foreach (var cabecalho in dto.Cabecalhos)
            {
                total = cabecalho.Transportadora.Cobrancas.Count;
                soma = cabecalho.Transportadora.Cobrancas.Sum(x => x.ValorTotal);
                foreach (var cobranca in cabecalho.Transportadora.Cobrancas)
                {
                    totalItens += cobranca.Conhecimentos.Count();
                    ArquivoCobrancaDocumento arquivoCobrancaDocumento = new ArquivoCobrancaDocumento(cobranca);
                    AdicionarArquivoCobrancaDocumento(arquivoCobrancaDocumento);
                }
            }
            AtualizarQtdTotal(total);
            AtualizarQtdItens(totalItens);
            AtualizarValorTotal(soma);
        }

        private void AdicionarArquivoCobrancaDocumento(ArquivoCobrancaDocumento arquivoCobrancaDocumento)
        {
            if (ArquivoCobrancaDocumentos == null)
                ArquivoCobrancaDocumentos = new List<ArquivoCobrancaDocumento>();
            ArquivoCobrancaDocumentos.Add(arquivoCobrancaDocumento);
        }

        #endregion

        #region "Propriedades"

        public int FaturaId { get; protected set; }
        public string IdentificacaoRemetente { get; protected set; }
        public string IdentificacaoDestinatario { get; protected set; }
        public DateTime Data { get; protected set; }
        public int QtdTotal { get; protected set; }
        public int QtdItens { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public string ArquivoUrl { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual List<ArquivoCobrancaDocumento> ArquivoCobrancaDocumentos { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarFaturaId(int faturaId) => this.FaturaId = faturaId;
        public void AtualizarIdentificacaoRemetente(string identificacaoRemetente) => this.IdentificacaoRemetente = identificacaoRemetente;
        public void AtualizarIdentificacaoDestinatario(string identificacaoDestinatario) => this.IdentificacaoDestinatario = identificacaoDestinatario;
        public void AtualizarData(DateTime data) => this.Data = data;
        public void AtualizarValorTotal(decimal valor) => this.ValorTotal = valor;
        public void AtualizarQtdTotal(int qtdTotal) => this.QtdTotal = qtdTotal;
        public void AtualizarQtdItens(int qtdItens) => this.QtdItens = qtdItens;
        public void AtualizarArquivoUrl(string arquivoUrl) => this.ArquivoUrl = arquivoUrl;
        #endregion
    }
}      
