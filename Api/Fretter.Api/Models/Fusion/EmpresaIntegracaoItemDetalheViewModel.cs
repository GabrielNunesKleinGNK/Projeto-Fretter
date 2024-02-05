using AutoMapper;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class EmpresaIntegracaoItemDetalheViewModel : IViewModel<EmpresaIntegracaoItemDetalhe>
    {
        public int Id { get; set; }
        public int EmpresaIntegracaoItemId { get;  set; }
        public long CodigoIntegracao { get;  set; }
        public string Chave { get;  set; }
        public string RequestURL { get;  set; }
        public string JsonEnvio { get;  set; }
        public string JsonBody { get;  set; }
        public string JsonRetorno { get;  set; }
        public long HttpTempo { get;  set; }
        public string HttpStatus { get;  set; }
        public string HttpResponse { get;  set; }
        public bool Sucesso { get;  set; }
        public bool PendenteProcessamentoRetorno { get;  set; }
        public bool ProcessadoRetornoSucesso { get;  set; }
        public string MensagemRetorno { get;  set; }
        public int HttpStatusCode { get;  set; }
        public bool Ativo{ get; set; }
        public DateTime DataCadastro { get; set; }

        public EmpresaIntegracaoItemDetalhe Model()
        {

            return new EmpresaIntegracaoItemDetalhe(Id, EmpresaIntegracaoItemId, CodigoIntegracao, Chave, RequestURL, JsonEnvio, JsonBody, JsonRetorno, HttpTempo,
                                            HttpStatus, HttpResponse, Sucesso, PendenteProcessamentoRetorno, ProcessadoRetornoSucesso, MensagemRetorno, HttpStatusCode);
        }
    }
}
