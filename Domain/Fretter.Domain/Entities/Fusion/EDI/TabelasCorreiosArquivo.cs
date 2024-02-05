using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class TabelasCorreiosArquivo : EntityBase
    {
        #region "Construtores"

        public TabelasCorreiosArquivo(){}
        public TabelasCorreiosArquivo(int Id, int idTabelaArquivoStatus, string dsNomeArquivo, string dsUrl)
        {
            this.Ativar();
            this.Id = Id;
            this.TabelaArquivoStatusId = idTabelaArquivoStatus;
            this.NomeArquivo = dsNomeArquivo;
            this.Url = dsUrl;
            this.Criacao = DateTime.Now;
        }

        public TabelasCorreiosArquivo(int Id, int idTabelaArquivoStatus, string dsNomeArquivo, string dsUrl, DateTime dtCriacao, DateTime? dtImportacaoDados, DateTime? dtAtualizacaoTabelas, string dsErro)
        {
            this.Ativar();
            this.Id = Id;
            this.TabelaArquivoStatusId = idTabelaArquivoStatus;
            this.NomeArquivo = dsNomeArquivo;
            this.Url = dsUrl;
            this.Criacao = dtCriacao;
            this.ImportacaoDados = dtImportacaoDados;
            this.AtualizacaoTabelas = dtAtualizacaoTabelas;
            this.Erro = dsErro;
        }
        #endregion

        #region "Propriedades"
        public int TabelaArquivoStatusId { get; protected set; }
        public string NomeArquivo { get; protected set; }
        public string Url { get; protected set; }
        public DateTime Criacao { get; protected set; }
        public DateTime? ImportacaoDados { get; protected set; }
        public DateTime? AtualizacaoTabelas { get; protected set; }
        public string Erro { get; protected set; }
        #endregion

        #region Referecncias
        public TabelaArquivoStatus TabelaArquivoStatus { get; set; }
        #endregion

        #region Métodos
        public void AtualizarDataImportacaoDados(DateTime? dtImportacaoDados) => this.ImportacaoDados = dtImportacaoDados;
        public void AtualizarDataAtualizacaoTabelas(DateTime? dtAtualizacaoTabelas) => this.AtualizacaoTabelas = dtAtualizacaoTabelas;
        public void AtualizarStatus(int IdTabelaArquivoStatus) => this.TabelaArquivoStatusId = IdTabelaArquivoStatus;
        public void AtualizarErro(string dsErro) => this.Erro = dsErro;
        #endregion
    }
}
