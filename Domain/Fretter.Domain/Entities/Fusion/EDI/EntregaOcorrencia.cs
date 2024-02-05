using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class EntregaOcorrencia : EntityBase
    {
        #region "Construtores"

        public EntregaOcorrencia(int Id, int? OcorrenciaId, string Ocorrencia,  DateTime DataOcorrencia, int EntregaId)
        {
            this.Id = Id;
            this.OcorrenciaId = OcorrenciaId;
            this.Ocorrencia = Ocorrencia;
            this.DataOcorrencia = DataOcorrencia;
            this.EntregaId = EntregaId;
        }

        public EntregaOcorrencia(int Id, int EntregaId, int? OcorrenciaId, string Ocorrencia, DateTime DataOcorrencia, int? TransportadorId, string Sigla, bool? Finalizar, string Uf, string Cidade, string Dominio, string Complementar)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaId = EntregaId;
            this.OcorrenciaId = OcorrenciaId;
            this.Ocorrencia = Ocorrencia;
            this.DataOcorrencia = DataOcorrencia;
            this.TransportadorId = TransportadorId;
            this.Sigla = Sigla;
            this.Finalizar = Finalizar;
            this.Uf = Uf;
            this.Cidade= Cidade;
            this.Dominio = Dominio;
            this.Complementar = Complementar;
            this.OcorrenciaStatusValidacao = true;
        }
        #endregion

        #region "Propriedades"
        public int EntregaId { get; protected set; }
        public int? OcorrenciaId { get; protected set; }
        public string Ocorrencia { get; protected set; }
        public DateTime DataOcorrencia { get; protected set; }
        public DateTime? DataInclusao { get; protected set; }
        public DateTime? DataOriginal { get; protected set; }
        public bool? OcorrenciaValidada { get; protected set; }
        public Guid? EdiId { get; protected set; }
        public string ArquivoImportacao { get; protected set; }
        public string UsuarioInclusao { get; protected set; }
        public bool? OcorrenciaValidadaDe { get; protected set; }
        [NotMapped]
        public int? OcorrenciaDeId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        [NotMapped]
        public int? OrigemImportacaoId { get; protected set; }
        public DateTime? DataInclusaoAnterior { get; protected set; }
        public DateTime? DataOcorrenciaAnterior { get; protected set; }
        [NotMapped]
        public string CodigoBaseTransportador { get; protected set; }
        public string Complementar { get; protected set; }
        [NotMapped]
        public int? ArquivoId { get; protected set; }
        [NotMapped]
        public DateTime? DataPostagemAtualiza { get; protected set; }
        public string Sigla { get; protected set; }
        public bool? Finalizar { get; protected set; }
        public string Dominio { get; protected set; }
        public string Cidade { get; protected set; }
        public string Uf { get; protected set; }
        public string Latitude { get; protected set; }
        public string Longitude { get; protected set; }
        [NotMapped]
        public string RetornoValidacao { get; protected set; }
        [NotMapped]
        public bool OcorrenciaStatusValidacao { get; protected set; } = true;
        #endregion

        #region "Referencias"

        #endregion

        #region "Métodos"
        public void AtualizarEntregaId(int EntregaId) => this.EntregaId = EntregaId;
        public void AtualizarOcorrenciaId(int? OcorrenciaId) => this.OcorrenciaId = OcorrenciaId;
        public void AtualizarOcorrencia(string Ocorrencia) => this.Ocorrencia = Ocorrencia;
        public void AtualizarDataOcorrencia(DateTime DataOcorrencia) => this.DataOcorrencia = DataOcorrencia;
        public void AtualizarDataInclusao(DateTime? DataInclusao) => this.DataInclusao = DataInclusao;
        public void AtualizarDataOriginal(DateTime? DataOriginal) => this.DataOriginal = DataOriginal;
        public void AtualizarOcorrenciaValidada(bool? OcorrenciaValidada) => this.OcorrenciaValidada = OcorrenciaValidada;
        public void AtualizarEdiId(Guid? EdiId) => this.EdiId = EdiId;
        public void AtualizarArquivoImportacao(string ArquivoImportacao) => this.ArquivoImportacao = ArquivoImportacao;
        public void AtualizarUsuarioInclusao(string UsuarioInclusao) => this.UsuarioInclusao = UsuarioInclusao;
        public void AtualizarOcorrenciaValidadaDe(bool? OcorrenciaValidadaDe) => this.OcorrenciaValidadaDe = OcorrenciaValidadaDe;
        public void AtualizarOcorrenciaDeId(int? OcorrenciaDeId) => this.OcorrenciaDeId = OcorrenciaDeId;
        public void AtualizarTransportadorId(int? TransportadorId) => this.TransportadorId = TransportadorId;
        public void AtualizarOrigemImportacaoId(int? OrigemImportacaoId) => this.OrigemImportacaoId = OrigemImportacaoId;
        public void AtualizarDataInclusaoAnterior(DateTime? DataInclusaoAnterior) => this.DataInclusaoAnterior = DataInclusaoAnterior;
        public void AtualizarDataOcorrenciaAnterior(DateTime? DataOcorrenciaAnterior) => this.DataOcorrenciaAnterior = DataOcorrenciaAnterior;
        public void AtualizarCodigoBaseTransportador(string CodigoBaseTransportador) => this.CodigoBaseTransportador = CodigoBaseTransportador;
        public void AtualizarComplementar(string Complementar) => this.Complementar = Complementar;
        public void AtualizarArquivoId(int? ArquivoId) => this.ArquivoId = ArquivoId;
        public void AtualizarDataPostagemAtualiza(DateTime? DataPostagemAtualiza) => this.DataPostagemAtualiza = DataPostagemAtualiza;
        public void AtualizarSigla(string Sigla) => this.Sigla = Sigla;
        public void AtualizarFinalizar(bool? Finalizar) => this.Finalizar = Finalizar;
        public void AtualizarDominio(string Dominio) => this.Dominio = Dominio;
        public void AtualizarCidade(string Cidade) => this.Cidade = Cidade;
        public void AtualizarUf(string Uf) => this.Uf = Uf;
        public void AtualizarLatitude(string Latitude) => this.Latitude = Latitude;
        public void AtualizarLongitude(string Longitude) => this.Longitude = Longitude;


        public void AtualizarRetornoValidacao(string RetornoValidacao) => this.RetornoValidacao = RetornoValidacao;
        public void AtualizarOcorrenciaStatusValidacao(bool OcorrenciaStatusValidacao) => this.OcorrenciaStatusValidacao = OcorrenciaStatusValidacao;
        #endregion
    }
}
