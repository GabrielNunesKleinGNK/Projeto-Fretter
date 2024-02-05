using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaIntegracaoItemDetalhe : EntityBase
    {
        #region "Construtores"
        public  EmpresaIntegracaoItemDetalhe() { }
        public EmpresaIntegracaoItemDetalhe(int Id, int EmpresaIntegracaoItemId, long CodigoIntegracao, string Chave, string RequestURL, string JsonEnvio, string JsonBody, string JsonRetorno, long HttpTempo,
                                            string HttpStatus, string HttpResponse, bool Sucesso, bool PendenteProcessamentoRetorno, bool ProcessadoRetornoSucesso, string MensagemRetorno, int HttpStatusCode)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaIntegracaoItemId = EmpresaIntegracaoItemId;
            this.CodigoIntegracao = CodigoIntegracao;
            this.Chave = Chave;
            this.RequestURL = RequestURL;
            this.JsonEnvio = JsonEnvio;
            this.JsonBody = JsonBody;
            this.JsonRetorno = JsonRetorno;
            this.HttpTempo = HttpTempo;
            this.HttpStatus = HttpStatus;
            this.HttpResponse = HttpResponse;
            this.Sucesso = Sucesso;
            this.PendenteProcessamentoRetorno = PendenteProcessamentoRetorno;
            this.ProcessadoRetornoSucesso = ProcessadoRetornoSucesso;
            this.MensagemRetorno = MensagemRetorno;
            this.HttpStatusCode = HttpStatusCode;
        }
        #endregion

        #region "Propriedades"  
        public int EmpresaIntegracaoItemId { get; protected set; }
        public long CodigoIntegracao { get; protected set; }
        public string Chave { get; protected set; }
        public string RequestURL { get; protected set; }
        public string JsonEnvio { get; protected set; }
        public string JsonBody { get; protected set; }
        public string JsonRetorno { get; protected set; }
        public long HttpTempo { get; protected set; }
        public string HttpStatus { get; protected set; }
        public string HttpResponse { get; protected set; }
        public bool Sucesso { get; protected set; }
        public bool PendenteProcessamentoRetorno { get; protected set; }
        public bool ProcessadoRetornoSucesso { get; protected set; }
        public string MensagemRetorno { get; protected set; }
        public int HttpStatusCode { get; protected set; }


        #endregion

        #region "Referencias"
        //public List<EmpresaIntegracaoItem> ListaIntegracoes { get; set; }

        #endregion

        #region "Métodos"
        public void AtualizarEmpresaIntegracaoItemId(int EmpresaIntegracaoItemId) => this.EmpresaIntegracaoItemId = EmpresaIntegracaoItemId;
        public void AtualizarCodigoIntegracao(long CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarChave(string Chave) => this.Chave = Chave;
        public void AtualizarRequestURL(string RequestURL) => this.RequestURL = RequestURL;
        public void AtualizarJsonEnvio(string JsonEnvio) => this.JsonEnvio = JsonEnvio;
        public void AtualizarJsonBody(string JsonBody) => this.JsonBody = JsonBody;
        public void AtualizarJsonRetorno(string JsonRetorno) => this.JsonRetorno = JsonRetorno;
        public void AtualizarHttpTempo(long HttpTempo) => this.HttpTempo = HttpTempo;
        public void AtualizarHttpStatus(string HttpStatus) => this.HttpStatus = HttpStatus;
        public void AtualizarHttpResponse(string HttpResponse) => this.HttpResponse = HttpResponse;
        public void AtualizarSucesso(bool Sucesso) => this.Sucesso = Sucesso;
        public void AtualizarPendenteProcessamentoRetorno(bool PendenteProcessamentoRetorno) => this.PendenteProcessamentoRetorno = PendenteProcessamentoRetorno;
        public void AtualizarProcessadoRetornoSucesso(bool ProcessadoRetornoSucesso) => this.ProcessadoRetornoSucesso = ProcessadoRetornoSucesso;
        public void AtualizarSMensagemRetorno(string MensagemRetorno) => this.MensagemRetorno = MensagemRetorno;
        public void AtualizarHttpStatusCode(int HttpStatusCode) => this.HttpStatusCode = HttpStatusCode;
        #endregion
    }
}

