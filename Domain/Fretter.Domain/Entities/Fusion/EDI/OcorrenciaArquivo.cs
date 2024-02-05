using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class OcorrenciaArquivo : EntityBase
    {
        #region "Construtores"
        public OcorrenciaArquivo()
        {

        }

        public OcorrenciaArquivo(int Id, int empresaId, int idTabelaArquivoStatus, string dsNomeArquivo, string dsUrl,  string dsUrlResultado, string usuario,
            DateTime? dtAtualizacao, int? qtAdvertencia, int? qtErro, int? qtRegistros, int? percentualAtualizacao)
        {
            this.Ativar();
            this.Id = Id;
            this.TabelaArquivoStatusId = idTabelaArquivoStatus;
            this.EmpresaId = empresaId;
            this.NomeArquivo = dsNomeArquivo;
            this.Url = dsUrl;
            this.Retorno = dsUrlResultado;
            this.QtErros = qtErro;
            this.QtRegistros = qtRegistros;
            this.QtAdvertencia = qtAdvertencia;
            this.PercentualAtualizacao = percentualAtualizacao;
            this.UltimaAtualizacao = dtAtualizacao;
            this.Usuario = usuario;
        }
        #endregion

        #region "Propriedades"
        public int TabelaArquivoStatusId { get; protected set; }
        public int EmpresaId { get; protected set; }
        public string NomeArquivo { get; protected set; }
        public string Url { get; protected set; }
        public string Retorno { get; protected set; }
        public int? QtAdvertencia { get; protected set; }
        public int? QtErros { get; protected set; }
        public int? QtRegistros { get; protected set; }
        public int? PercentualAtualizacao { get; protected set; }
        public DateTime? UltimaAtualizacao { get; protected set; }
        public string Usuario { get; protected set; }
        #endregion

        public void AtualizarTabelaArquivoStatusId(int TabelaArquivoStatusId) => this.TabelaArquivoStatusId = TabelaArquivoStatusId;
        public void AtualizarEmpresaId(int EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarDs_NomeArquivo(string NomeArquivo) => this.NomeArquivo = NomeArquivo;
        public void AtualizarDs_Url(string Url) => this.Url = Url;
        public void AtualizarOb_JsonRetorno(string Retorno) => this.Retorno = Retorno;
        public void AtualizarQt_Advertencia(int? QtAdvertencia) => this.QtAdvertencia = QtAdvertencia;
        public void AtualizarQt_Erros(int? QtErros) => this.QtErros = QtErros;
        public void AtualizarQt_Registros(int? QtRegistros) => this.QtRegistros = QtRegistros;
        public void AtualizarNr_PercentualAtualizacao(int? PercentualAtualizacao) => this.PercentualAtualizacao = PercentualAtualizacao;
        public void AtualizarDt_UltimaAtualizacao(DateTime? UltimaAtualizacao) => this.UltimaAtualizacao = UltimaAtualizacao;
        public void AtualizarNm_Usuario(string Usuario) => this.Usuario = Usuario;


    }
}
